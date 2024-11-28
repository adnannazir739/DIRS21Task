using System;

namespace DynamicMappingSystem
{
   
    public interface IMapping
    {
        string SourceType { get; }
        string TargetType { get; }
        object Map(object source);
    }

}
