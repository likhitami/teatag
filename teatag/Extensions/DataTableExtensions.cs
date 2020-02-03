using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;

namespace teatag.Extensions
{
    public static class DataTableExtensions
    {
        public static List<T> ToList<T>(this DataTable table) where T : class, new()
        {
            try
            {
                List<T> list = new List<T>();

                foreach (var row in table.AsEnumerable())
                {

                    T obj = new T();
                    PropertyInfo[] props = obj.GetType().GetProperties();
                    foreach (var prop in props)
                    {
                        try
                        {
                            PropertyInfo propertyInfo = obj.GetType().GetProperty(prop.Name);
                            propertyInfo.SetValue(obj, Convert.ChangeType(row[prop.Name], propertyInfo.PropertyType), null);
                        }
                        catch
                        {
                            continue;
                        }
                    }

                    list.Add(obj);
                }

                return list;
            }
            catch
            {
                return null;
            }
        }
    }
}
