using System;
using System.ComponentModel.DataAnnotations;

namespace Ryanstaurant.GMS.DataAccess.EF
{
    public partial class GMS_Token
    {
        [Key]
        public Guid TokenKey { get; set; }

        public DateTime ExpireTime { get; set; }

        public DateTime CreateTime { get; set; }

    }
}
