using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

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
    public class RequestEntitiy<T>
    {
        [DataMember]
        public List<RequestContent<T>> RequestContents { get; set; } 




        public List<T> QueryContentList
        {
            get
            {
                
                if (RequestContents.Count == 1 && RequestContents[0].RequestObject == null)
                {
                    return new List<T>();
                }

                var queries = from rc in RequestContents
                    where rc.Operation == RequestOperation.Query && rc.RequestObject != null
                    select rc.RequestObject;

                return queries.Any() ? queries.ToList() : null;
            }
        }

    }
}
