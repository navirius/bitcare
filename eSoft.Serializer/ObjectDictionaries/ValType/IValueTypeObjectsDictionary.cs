using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eSoft.Serializer.TypeSerializers;

namespace eSoft.Serializer.ObjectDictionaries.ValType
{
    public interface IValueTypeObjectsDictionary
    {
        bool GetObjectIdForValueTypeField<T4PO>(T4PO processedObject, ICachedSerializedObject valueSerializer);
        T4PO GetObjectValueForValueTypeField<T4PO>(ICachedSerializedObject valueSerializer);
        void RegisterValue<T4PO>(T4PO processedObject, ICachedSerializedObject valueSerializer);
        void Reset();
    }
}
