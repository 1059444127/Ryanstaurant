using System.Runtime.Serialization;

namespace Ryanstaurant.UMS.DataContract.Utility
{
    [DataContract]
    public class ResultItem<T>
    {
        [DataMember]
        public T Item { get; set; }


        [DataMember]
        public string Exception { get; set; }


    }
}
