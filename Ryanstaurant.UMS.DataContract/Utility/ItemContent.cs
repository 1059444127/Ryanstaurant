using System.Runtime.Serialization;

namespace Ryanstaurant.UMS.DataContract.Utility
{
    [DataContract]
    [KnownType(typeof(Employee))]
    [KnownType(typeof(Role))]
    [KnownType(typeof(Authority))]
    [KnownType(typeof(Login))]
    public class ItemContent
    {
        [DataMember]
        public ResultContent ResultInfo { get; set; }

        [DataMember]
        public RequestContent RequestInfo { get; set; }


        public void SetAddState()
        {
            this.RequestInfo = new RequestContent
            {
                Operation = RequestOperation.Add
            };
        }
        public void SetModifyState()
        {
            this.RequestInfo = new RequestContent
            {
                Operation = RequestOperation.Modify
            };
        }
        public void SetDeleteState()
        {
            this.RequestInfo = new RequestContent
            {
                Operation = RequestOperation.Delete
            };
        }
        public void SetQueryState()
        {
            this.RequestInfo = new RequestContent
            {
                Operation = RequestOperation.Query
            };
        }

    }
}
