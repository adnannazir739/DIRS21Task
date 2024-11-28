using System;

namespace DynamicMappingSystem
{
    public interface IDataModel { }

    public interface IMapping
    {
        string SourceType { get; }
        string TargetType { get; }
        object Map(object source);
    }

    public interface IMapHandler
    {
        void RegisterMapping(IMapping mapping);
        object Map(object data, string sourceType, string targetType);
    }
}
