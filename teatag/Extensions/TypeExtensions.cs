using System;

namespace teatag.Extensions
{
    public static class TypeExtensions
    {
        //https://stackoverflow.com/questions/42809686/c-sharp-compare-two-objects-for-properties-with-different-values
        public static bool IsSimpleType(this Type type)
        {
            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
            {
                // nullable type, check if the nested type is simple.
                return type.GetGenericArguments()[0].IsSimpleType();
            }
            return type.IsPrimitive
              || type.IsEnum
              || type.Equals(typeof(string))
              || type.Equals(typeof(decimal));
        }
    }
}
