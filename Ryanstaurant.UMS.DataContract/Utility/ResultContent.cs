using System.Runtime.Serialization;

namespace Ryanstaurant.UMS.DataContract.Utility
{
    [DataContract]
    public class ResultContent<T>
    {
        [DataMember]
        public T Item { get; set; }


        [DataMember]
        public string Exception { get; set; }


        [DataMember]
        public ResultState State { get; set; }

        [DataMember]
        public string InnerErrorMessage { get; set; }



    }
}
