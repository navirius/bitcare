using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace eSoft.T4.Core
{
    public static class FieldTypeInfoExtensions
    {
        public static string GetSafeName(this FieldInfo fieldInfo) 
        { 
            return fieldInfo.Name.Replace("[]", "_Array").Replace(".", "_").Replace("<", "_").Replace(">", "_"); 
        }
    }
}
