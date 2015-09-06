using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Ryanstaurant.UMS.DataContract.Utility
{
    public enum ResultState
    {
        Success = 1,
        Fail = 0
    }

    [DataContract]
    public class ResultEntity
    {

        [DataMember]
        public string ErrorMessage { get; set; }


        [DataMember]
        public ResultState State { get; set; }

        [DataMember]
        public List<ItemContent> ResultObject { get; set; }

        [DataMember]
        public string InnerErrorMessage { get; set; }


    }
}
