using System;

namespace Ryanstaurant.UMS.Interface
{

    public enum ExceptionType
    {
        None = 0,
        AlreadyExist = 1,
        NotExist = 2,
        Failed = 3,
        Unknown = 9999
    }



    public interface IEntity
    {
        string Exception { get; set; }

        ExceptionType ExpType { get; set; }
        string ExceptionStackTrace { get; set; }
    }


    public static class EntityAdaptor
    {

        public static T ConvertToDataContract<T>(this IEntity iEntity)
        {
            var dataContractProperties = typeof (T).GetProperties();
            var dataContractObject = Activator.CreateInstance(typeof (T));
            foreach (var dataContractProperty in dataContractProperties)
            {
                var entitiyProperty = iEntity.GetType().GetProperty(dataContractProperty.Name);

                if (entitiyProperty == null)
                    continue;

                dataContractProperty.SetValue(dataContractObject, entitiyProperty.GetValue(iEntity, null), null);
            }
            return (T) dataContractObject;
        }
    }
}
