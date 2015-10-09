namespace Ryanstaurant.OMS.DataAccess
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class OMS_Token
    {
        [Key]
        public Guid TokenKey { get; set; }

        public DateTime ExpireTime { get; set; }
    }
}
