using System;
using System.ComponentModel;

namespace DutyBoard_Models
{
    public class TableProperty
    {
        public PropertyDescriptor Descriptor;
        public string Name;
        public TableProperty() { }
        public TableProperty(PropertyDescriptor descriptor, string name) =>
            (Descriptor, Name) = (descriptor, name);
        public Type PropertyType => Nullable.GetUnderlyingType(Descriptor.PropertyType) ?? Descriptor.PropertyType;
        public object GetValue(object item) => Descriptor.GetValue(item);
    }
}
