// -----------------------------------------------------------------------
// <copyright file="eSoftSerializer.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace eSoft.Serializer.ObjectDictionaries.ValType
{
    public class ValTypeDictEntry<T>
    {
        // Dictionary with values for ValueType
        public Dictionary<T, int> ValDict;

        // Dictionary with Id for ValueType
        public Dictionary<int, T> IdDict;

        // Default constructor
        public ValTypeDictEntry()
        {
            ValDict = new Dictionary<T, int>();
            IdDict = new Dictionary<int, T>();
        }

        // Every ValueType object we numerate from 1 (every type has its own range)
        private int m_ValTypeSpecificNextObjectId = 1;

        // Get next object id
        public int GetNextObjectId()
        {
            return m_ValTypeSpecificNextObjectId++;
        }
    }
}
