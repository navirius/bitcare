using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eSoft.Serializer.TypeSerializers;

namespace eSoft.Serializer.ObjectDictionaries.RefType
{
    public interface IRefTypeObjectsDictionary
    {
        bool GetObjectIdForRefTypeField<T4PO>(T4PO processedObject, ICachedSerializedObject valueSerializer);
        T4PO GetObjectValueForRefTypeField<T4PO>(ICachedSerializedObject valueSerializer);
        void RegisterValue<T4PO>(T4PO processedObject, ICachedSerializedObject valueSerializer);
        void Reset();
    }
}
