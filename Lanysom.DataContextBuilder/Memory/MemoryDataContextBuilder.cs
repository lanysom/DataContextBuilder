using Newtonsoft.Json;
using System;
using System.IO;
using System.Reflection;

namespace Lanysom.DataContextBuilder.Memory
{
    class MemoryDataContextBuilder : DataContextBuilder
    {
        public override DataContextBuilder Feed<TEntity>(string pathToTestData)
        {
            string json = File.ReadAllText(pathToTestData);
            var jsonObject = JsonConvert.DeserializeObject<dynamic>(json);

            string type = jsonObject.type;
            var data = jsonObject.data;

            var entities = _context.GetType().GetProperty(type).GetValue(_context);
            var addMethod = entities.GetType().GetMethod("Add");

            foreach (var row in data)
            {
                object obj = new TEntity();
                Type entityType = obj.GetType();

                foreach (var item in row)
                {
                    var name = item.Name;
                    var value = item.Value.Value;

                    if (value == null) continue;

                    PropertyInfo propertyInfo = entityType.GetProperty(name);
                    
                    if (propertyInfo == null) continue;
                    
                    Type propertyType = propertyInfo.PropertyType;
                    Type targetType = propertyType.IsGenericType && propertyType.GetGenericTypeDefinition().Equals(typeof(Nullable<>)) ?
                        Nullable.GetUnderlyingType(propertyType) : propertyType;
                    value = Convert.ChangeType(value, targetType);
                    propertyInfo.SetValue(obj, value);

                }
                addMethod.Invoke(entities, new[] { obj });
            }

            return this;
        }
    }
}
