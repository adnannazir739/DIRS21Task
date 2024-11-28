using System;

namespace DynamicMappingSystem
{
    public class Mapping : IMapping
    {
        public string SourceType { get; }
        public string TargetType { get; }
        private readonly Func<object, object> _mapFunction;

        public Mapping(string sourceType, string targetType, Func<object, object> mapFunction)
        {
            SourceType = sourceType;
            TargetType = targetType;
            _mapFunction = mapFunction;
        }

        public object Map(object source)
        {
            try
            {
                return _mapFunction(source);
            }
            catch (Exception ex)
            {
                throw new ArgumentException($"Mapping failed from {SourceType} to {TargetType}.", ex);
            }
        }
    }
}
