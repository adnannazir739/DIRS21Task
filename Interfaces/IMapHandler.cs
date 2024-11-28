using System;

namespace DynamicMappingSystem
{
    
    public interface IMapHandler
    {
        void RegisterMapping(IMapping mapping);
        object Map(object data, string sourceType, string targetType);
    }
}
