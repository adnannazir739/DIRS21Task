using System;
using System.Collections.Generic;
using System.Linq;

namespace DynamicMappingSystem
{
    public class MapHandler : IMapHandler
    {
        private readonly List<IMapping> _mappings;

        public MapHandler()
        {
            _mappings = new List<IMapping>();
        }

        public void RegisterMapping(IMapping mapping)
        {
            if (_mappings.Any(m => m.SourceType == mapping.SourceType && m.TargetType == mapping.TargetType))
            {
                throw new InvalidOperationException($"Mapping from {mapping.SourceType} to {mapping.TargetType} already exists.");
            }
            _mappings.Add(mapping);
        }

        public object Map(object data, string sourceType, string targetType)
        {
            var mapping = _mappings.FirstOrDefault(m => m.SourceType == sourceType && m.TargetType == targetType);

            if (mapping == null)
                throw new InvalidOperationException($"Mapping not found from {sourceType} to {targetType}.");

            return mapping.Map(data);
        }
    }
}
