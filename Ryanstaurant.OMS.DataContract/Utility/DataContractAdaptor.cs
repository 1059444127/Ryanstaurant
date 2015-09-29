using System;

namespace Ryanstaurant.OMS.DataContract.Utility
{
    public static class DataContractAdaptor
    {
        public static T ConvertTo<T>(object Object, Type type) where T : new()
        {
            var entity = new T();
            var properties = type.GetProperties();
            foreach (var propertyInfo in properties)
            {
                var mapAttrs = propertyInfo.GetCustomAttributes(typeof (EntityMappingAttribute), false);
                if (mapAttrs.Length == 0 || (mapAttrs[0] as EntityMappingAttribute) == null)
                    continue;

                var propertyName = string.IsNullOrEmpty((mapAttrs[0] as EntityMappingAttribute).MapName)
                    ? propertyInfo.Name
                    : (mapAttrs[0] as EntityMappingAttribute).MapName;

                var entityProperty = entity.GetType().GetProperty(propertyName);
                if (entityProperty == null) continue;
                var value = propertyInfo.GetValue(Object, null);

                if (((mapAttrs[0] as EntityMappingAttribute).MapType == null)||(mapAttrs[0] as EntityMappingAttribute).MapType==typeof(string))
                {
                    entityProperty.SetValue(entity, value, null);
                }
                else
                {
                    object entityValue;
                    try
                    {
                        entityValue = entityProperty.PropertyType.GetMethod("Parse", new[] { propertyInfo.PropertyType })
                        .Invoke(null, new[] { value });
                    }
                    catch (Exception ex)
                    {
                        throw new InvalidCastException(propertyInfo.Name + "->" + entityProperty.Name, ex);
                    }

                    entityProperty.SetValue(entity, entityValue, null);
                }
            }
            return entity;
        }


        public static T LoadFrom<T>(object entity, Type type) where T : new()
        {
            var dataContract = new T();
            var properties = typeof(T).GetProperties();
            foreach (var propertyInfo in properties)
            {
                var mapAttrs = propertyInfo.GetCustomAttributes(typeof(EntityMappingAttribute), false);
                if (mapAttrs.Length == 0 || (mapAttrs[0] as EntityMappingAttribute) == null)
                    continue;

                var propertyName = string.IsNullOrEmpty((mapAttrs[0] as EntityMappingAttribute).MapName)
                    ? propertyInfo.Name
                    : (mapAttrs[0] as EntityMappingAttribute).MapName;

                var entityProperty = entity.GetType().GetProperty(propertyName);
                if (entityProperty == null) continue;
                var value = entityProperty.GetValue(entity, null);


                if (propertyInfo.PropertyType == typeof (string))
                {
                    propertyInfo.SetValue(dataContract, value.ToString(), null);
                }
                else if (((mapAttrs[0] as EntityMappingAttribute).MapType == null))
                {
                    propertyInfo.SetValue(dataContract, value, null);
                }
                else
                {
                    object dataContractValue;
                    try
                    {
                        dataContractValue = propertyInfo.PropertyType.GetMethod("Parse", new[] { entityProperty.PropertyType })
                        .Invoke(null, new[] { value});
                    }
                    catch (Exception ex)
                    {
                        throw new InvalidCastException(entityProperty.Name + "->" + propertyInfo.Name, ex);
                    }

                    propertyInfo.SetValue(dataContract, dataContractValue, null);
                }

            }
            return dataContract;
        }
    }
}
