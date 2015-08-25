using System;

namespace Ryanstaurant.UMS.Interface
{
    public interface IDataContract
    {
    }



    public static class DataContractAdaptor
    {


        public static T ConvertToDataContract<T>(this IDataContract iDataContract)
        {
            var entityProperties = typeof(T).GetProperties();

            var entityObject = Activator.CreateInstance(typeof(T));
            foreach (var entityProperty in entityProperties)
            {
                var dataContractProperty = iDataContract.GetType().GetProperty(entityProperty.Name);

                if (dataContractProperty == null)
                    continue;

                entityProperty.SetValue(entityObject, dataContractProperty.GetValue(iDataContract, null), null);
            }
            return (T) entityObject;
        }
    }
}
