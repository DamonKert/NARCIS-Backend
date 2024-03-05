using System.Reflection;

namespace NarcisKH.Class
{
    public class ValidateHelper
    {
        public static List<string> ValidateObject<T>(T obj)
        {
            List<string> errors = new List<string>();

            if (obj == null)
            {
                errors.Add("Object is null");
                return errors;
            }

            PropertyInfo[] properties = typeof(T).GetProperties();

            foreach (PropertyInfo property in properties)
            {
                object value = property.GetValue(obj);

                if (value == null || value.Equals(GetDefault(property.PropertyType)))
                {
                    errors.Add($"{property.Name} field is required");
                }
            }

            return errors;
        }

        private static object GetDefault(Type type)
        {
            return type.IsValueType ? Activator.CreateInstance(type) : null;
        }
    }

    // Example usage:
    public class ExampleObject
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }

}

