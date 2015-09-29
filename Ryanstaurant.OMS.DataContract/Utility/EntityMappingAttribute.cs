using System;
using System.Collections.Generic;

namespace Ryanstaurant.OMS.DataContract.Utility
{

    public class EntityMappingAttribute:Attribute
    {
        public string MapName { get; set; }

        public Type MapType { get; set; }
    }
}
