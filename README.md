# **Dynamic Mapping System**

## **Overview**
The **Dynamic Mapping System** provides a dynamic and extensible solution for mapping data between object models. It uses **Interfaces and Dependency Injection** for flexibility and try to follows best practices for code readability, maintainability, and extensibility.

---

## **System Architecture**

### **Key Components**

#### 1. **Models (Data Objects)**
Models represent entities involved in data mapping. 
- **Reservation:** Internal system data model.
- **GoogleReservation:** External system data model.

#### 2. **Interfaces**
Interfaces define the contracts for extensibility and enforce a consistent structure across the system.
- **`IDataModel`**: Base interface for all data models.
- **`IMapping`**: Represents a single mapping operation, including source type, target type, and mapping logic.
- **`IMapHandler`**: Manages all registered mappings and provides methods to perform mappings dynamically.

#### 3. **Helper**
The **Helper** folder contains utility classes for system functionality.
- **`Mapping`**: Implements `IMapping` to define mappings between two types.

#### 4. **Core Components**
- **`MapHandler`**: Implements `IMapHandler`. Responsible for registering mappings, validating input, and executing mapping functions.

#### 5. **Startup**
The `Startup` class configures **Dependency Injection** and registers services required by the system.

#### 6. **Program**
The entry point for the system. It demonstrates registering mappings and performing conversions between the data models.

---

## **How It Works**

1. **Dependency Injection Configuration**:
   - Configured in `Startup.cs` to ensure flexibility and extensibility.
   - Registers `MapHandler` as the implementation of `IMapHandler`.

2. **Mapping Registration**:
   - Mappings are registered using the `MapHandler.RegisterMapping` method.
   - Example:
     ```csharp
     mapHandler.RegisterMapping(new Mapping(
         typeof(Reservation).FullName,
         typeof(GoogleReservation).FullName,
         data =>
         {
             var reservation = (Reservation)data;
             return new GoogleReservation { GoogleBookingId = reservation.ReservationId };
         }));
     ```

3. **Performing Mapping**:
   - Use `MapHandler.Map` to convert an object from one type to another.
   - Example:
     ```csharp
     var reservation = new Reservation { ReservationId = "123" };
     var googleReservation = (GoogleReservation)mapHandler.Map(
         reservation,
         typeof(Reservation).FullName,
         typeof(GoogleReservation).FullName);
     ```

---

## **Instructions for Extending the System**

1. **Add a New Model**:
   - Create a new class in the `Models` folder that implements `IDataModel`.

2. **Extend Interfaces**:
   - Extend interfaces as needed to support additional operations..

3. **Extend the MapHandler**:
   - Add additional validation, logging, or features by modifying the `MapHandler` class.

---

## **Assumptions**
- Models have unique type identifiers (`FullName`).
- All mappings are registered before being used.

---

## **Limitations**
- The system currently does not support nested object mappings.
- Passing incorrect types during mapping will result in runtime errors. Ensure type safety before invoking the mapping logic.
- It requires explicit registration for every mapping, which may become cumbersome for a high number of models.

---

## **Example Code**
### Usage Instructions:
1. Setting Up Dependency Injection:
   The system requires Microsoft.Extensions.DependencyInjection for its DI setup. Install the package :
 ```csharp 
  dotnet add package Microsoft.Extensions.DependencyInjection
```
2. Configuring Services :
   In Startup.cs, configure the services:

```csharp
using Microsoft.Extensions.DependencyInjection;

public static class Startup
{
    public static IServiceProvider ConfigureServices()
    {
        var services = new ServiceCollection();
        services.AddSingleton<IMapHandler, MapHandler>();
        return services.BuildServiceProvider();
    }
}
```
## Performing a Mapping

This section provides a step-by-step example of how to perform a mapping using the system.

### Example: Reservation to GoogleReservation Mapping

The following example demonstrates how to use the system to map between two models: `Reservation` and `GoogleReservation`.

### Step 1: Register a Mapping
Before performing a mapping, register the source and target types with the `MapHandler`.

```csharp
var serviceProvider = Startup.ConfigureServices();
var mapHandler = serviceProvider.GetService<IMapHandler>();
mapHandler.RegisterMapping(new Mapping(
    typeof(Reservation).FullName,
    typeof(GoogleReservation).FullName,
    data =>
    {
        var reservation = (Reservation)data;
        return new GoogleReservation { GoogleBookingId = reservation.ReservationId };
    }));
```
---
### Step 2: Perform the Mapping

After registering the mapping, you can perform the mapping operation. Use the `Map` method of the `MapHandler` instance by passing the source object and the fully qualified type names for the source and target models.

```csharp
var reservation = new Reservation { ReservationId = "123" };
var googleReservation = (GoogleReservation)mapHandler.Map(
    reservation,
    typeof(Reservation).FullName,
    typeof(GoogleReservation).FullName);

Console.WriteLine($"Mapped GoogleReservation ID: {googleReservation.GoogleBookingId}");
```

### Example Output:

Mapped GoogleReservation ID: 123






