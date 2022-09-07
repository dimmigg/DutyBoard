using DutyBoard_Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;

namespace DutyBoard_Utility.Extensions
{
    public static class EnumerableExtensions
    {
        public static DataTable AsTable<T>(this IEnumerable<T> data)
        {
            var result = new DataTable();
            var properties = new List<TableProperty>();

            foreach (PropertyDescriptor descriptor in TypeDescriptor.GetProperties(typeof(T)))
            {
                var name = descriptor.Name;
                var property = new TableProperty(descriptor, name);
                result.Columns.Add(name, property.PropertyType);
                properties.Add(property);
            }

            foreach (T item in data)
            {
                var row = result.NewRow();

                foreach (var property in properties)
                {
                    row[property.Name] = property.GetValue(item) ?? DBNull.Value;
                }
                result.Rows.Add(row);
            }
            return result;
        }

        public static T Random<T>(this IEnumerable<T> enumerable)
        {
            if (enumerable == null)
            {
                throw new ArgumentNullException(nameof(enumerable));
            }

            var r = new Random();
            var list = enumerable as IList<T> ?? enumerable.ToList();
            return list.Count == 0 ? default : list[r.Next(0, list.Count)];
        }
    }
}
