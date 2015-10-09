namespace Ryanstaurant.UMS.DataAccess
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class UMS_Token
    {
        [Key]
        public Guid TokenKey { get; set; }

        public DateTime ExpireTime { get; set; }
    }
}
