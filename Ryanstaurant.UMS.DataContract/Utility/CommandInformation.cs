using System.Runtime.Serialization;

namespace Ryanstaurant.UMS.DataContract.Utility
{
    public enum RequestOperation
    {
        Query,
        Add,
        Modify,
        Delete
    }


    [DataContract]
    public class CommandInformation
    {
        [DataMember]
        public string Exception { get; set; }


        [DataMember]
        public ResultState State { get; set; }

        [DataMember]
        public string InnerErrorMessage { get; set; }


        [DataMember]
        public RequestOperation Operation { get; set; }

    }
}
