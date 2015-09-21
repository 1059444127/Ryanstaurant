using System.Runtime.Serialization;

namespace Ryanstaurant.UMS.DataContract.Utility
{
    [DataContract]
    [KnownType(typeof(Employee))]
    [KnownType(typeof(Role))]
    [KnownType(typeof(Authority))]
    public class ItemContent
    {
        [DataMember]
        public CommandInformation CommandInfo { get; set; }


        public void SetAddState()
        {
            CommandInfo = new CommandInformation
            {
                Operation = RequestOperation.Add
            };
        }
        public void SetModifyState()
        {
            CommandInfo = new CommandInformation
            {
                Operation = RequestOperation.Modify
            };
        }
        public void SetDeleteState()
        {
            CommandInfo = new CommandInformation
            {
                Operation = RequestOperation.Delete
            };
        }
        public void SetQueryState()
        {
            CommandInfo = new CommandInformation
            {
                Operation = RequestOperation.Query
            };
        }

    }
}
