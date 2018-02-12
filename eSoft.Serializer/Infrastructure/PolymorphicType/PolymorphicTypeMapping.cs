using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eSoft.Serializer.Exceptions;

namespace eSoft.Serializer.Infrastructure.PolimorphicType
{
    public class PolymorphicTypeMapping
    {
        private Dictionary<RuntimeTypeHandle, int> m_TypeMapping;

        public PolymorphicTypeMapping()
        {
            m_TypeMapping = new Dictionary<RuntimeTypeHandle, int>();
        }

        // Every ValueType object we numerate from 1 (every type has its own range)
        private int m_NextId = 1;

        // Get next object id
        public int GetNextId()
        {
            return ++m_NextId;
        }

        public void AddTypeMapping(RuntimeTypeHandle typeToMap)
        {
            m_TypeMapping.Add(typeToMap, GetNextId());
        }

        public int GetTypeMapping(RuntimeTypeHandle typeToMap)
        {
            if (m_TypeMapping.ContainsKey(typeToMap))
                return m_TypeMapping[typeToMap];

            throw new UnknownPolymorphicTypeException();
        }
    }
}
