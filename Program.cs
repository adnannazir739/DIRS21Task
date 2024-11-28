using System;
using Microsoft.Extensions.DependencyInjection;

namespace DynamicMappingSystem
{
    public class Program
    {
        static void Main(string[] args)
        {
            var serviceProvider = Startup.ConfigureServices();
            var mapHandler = serviceProvider.GetService<IMapHandler>();

            // Register mappings
            // Reservation --> GoogleReservation
            mapHandler.RegisterMapping(new Mapping(
                typeof(Reservation).FullName,
                typeof(GoogleReservation).FullName,
                data =>
                {
                    var reservation = (Reservation)data;
                    return new GoogleReservation { GoogleBookingId = reservation.ReservationId };
                }));
            // GoogleReservation --> Reservation
            mapHandler.RegisterMapping(new Mapping(
                typeof(GoogleReservation).FullName,
                typeof(Reservation).FullName,
                data =>
                {
                    var googleReservation = (GoogleReservation)data;
                    return new Reservation { ReservationId = googleReservation.GoogleBookingId };
                }));

            // Test mappings
            var reservation = new Reservation { ReservationId = "123" };
            var googleReservation = (GoogleReservation)mapHandler.Map(
                reservation,
                typeof(Reservation).FullName,
                typeof(GoogleReservation).FullName);

            Console.WriteLine($"Mapped GoogleReservation ID: {googleReservation.GoogleBookingId}");

            var newGoogleReservation = new GoogleReservation { GoogleBookingId = "456" };
            var newReservation = (Reservation)mapHandler.Map(
                newGoogleReservation,
                typeof(GoogleReservation).FullName,
                typeof(Reservation).FullName);

            Console.WriteLine($"Mapped Reservation ID: {newReservation.ReservationId}");
        }
    }
}
