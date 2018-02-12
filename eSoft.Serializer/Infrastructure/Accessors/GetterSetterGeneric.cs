using System.Reflection;

namespace eSoft.Serializer.Infrastructure.Accessors
{
    public class GetterSetterGeneric<TParentType, TMemberType>
    {
        private FieldInfo m_FieldInfo;
        public GetterSetterGeneric(FieldInfo fieldInfo)
        {
            m_FieldInfo = fieldInfo;
        }

        public TMemberType GetFieldValue(TParentType obj)
        {
            return (TMemberType)m_FieldInfo.GetValue(obj);
        }

        public void SetFieldValue(TParentType obj, TMemberType value)
        {
            m_FieldInfo.SetValue(obj, value);
        }
    }
}
