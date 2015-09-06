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
    public class RequestContent
    {
        [DataMember]
        public RequestOperation Operation { get; set; }

    }
}
