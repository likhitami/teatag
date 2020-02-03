using System;
using System.Linq;
using System.Reflection;
using teatag.Extensions;

namespace teatag.Utilities
{
    public class ObjectDifferent
    {
        //https://stackoverflow.com/questions/42809686/c-sharp-compare-two-objects-for-properties-with-different-values
        public static object Diff<T>(T Original, T compare)
        {
            if (Original != null && compare != null)
            {
                var type = typeof(T);
                var allProperties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
                var allSimpleProperties = allProperties.Where(pi => pi.PropertyType.IsSimpleType());
                var unequalProperties = (
                       from pi in allSimpleProperties
                       let AValue = type.GetProperty(pi.Name).GetValue(Original, null)
                       let BValue = type.GetProperty(pi.Name).GetValue(compare, null)
                       where AValue != BValue && (AValue == null || !AValue.Equals(BValue))
                       select new DiffProperty
                       {
                           Name = pi.Name,
                           Origin = AValue,
                           Changed = BValue
                       }).ToList();
                return unequalProperties;
            }
            else
            {
                throw new ArgumentNullException();
            }
        }

        public class DiffProperty
        {
            public string Name { get; set; }
            public object Origin { get; set; }
            public object Changed { get; set; }
        }
    }
}
