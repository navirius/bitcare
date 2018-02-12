using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Reflection.Emit;

namespace eSoft.Serializer.Infrastructure.Accessors
{
    public class FieldAccessor<TParentType, TMemberType>
    {
        /*   Getter  */
        public delegate TMemberType GetterDelegate(TParentType serializedObject);
        private GetterDelegate m_Getter;

        public GetterDelegate Get { get { return m_Getter; } }

        /*  Setter  */
        public delegate void SetterDelegate(TParentType serializedObject, TMemberType valueToSet);
        private SetterDelegate m_Setter;

        public SetterDelegate Set { get { return m_Setter; } }

#if SILVERLIGHT

        private GetterSetterGeneric<TParentType, TMemberType> m_GetterSetterGeneric;

        // Silverlight constructor
        public FieldAccessor(string memberName)
        {
            Type meta = typeof(TParentType);
            var fieldMeta = meta.GetField(memberName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy);
            m_GetterSetterGeneric = new GetterSetterGeneric<TParentType, TMemberType>(fieldMeta);

            // Getter
            m_Getter = m_GetterSetterGeneric.GetFieldValue;

            // Setter
            m_Setter = m_GetterSetterGeneric.SetFieldValue;
        }
#else
        // .Net Framework constructor
        public FieldAccessor(string memberName)
        {
            Type meta = typeof(TParentType);
            var fieldMeta = meta.GetField(memberName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy);

            // Getter
            DynamicMethod dmGetter = new DynamicMethod("", typeof(TMemberType), new Type[] { typeof(TParentType) }, true);
            ILGenerator ilGetter = dmGetter.GetILGenerator();
            ilGetter.Emit(OpCodes.Ldarg_0);
            ilGetter.Emit(OpCodes.Ldfld, fieldMeta);
            ilGetter.Emit(OpCodes.Ret);
            m_Getter = (GetterDelegate)dmGetter.CreateDelegate(typeof(GetterDelegate));

            // Setter
            DynamicMethod dmSetter = new DynamicMethod("", null, new Type[] { typeof(TParentType), typeof(TMemberType) }, true);
            ILGenerator ilSetter = dmSetter.GetILGenerator();
            ilSetter.Emit(OpCodes.Ldarg_0);
            ilSetter.Emit(OpCodes.Ldarg_1);
            ilSetter.Emit(OpCodes.Stfld, fieldMeta);
            ilSetter.Emit(OpCodes.Ret);

            m_Setter = (SetterDelegate)dmSetter.CreateDelegate(typeof(SetterDelegate));
        }
#endif
    }
}
