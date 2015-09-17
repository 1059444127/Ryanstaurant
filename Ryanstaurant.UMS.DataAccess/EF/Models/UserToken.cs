using System.Data.SqlTypes;

namespace Ryanstaurant.UMS.DataAccess.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("UserToken")]
    public partial class UserToken
    {
        [Key]
        [StringLength(36)]
        public string TokenKey { get; set; }

        private DateTime _createTime = SqlDateTime.MinValue.Value;

        public DateTime CreateTime
        {
            get
            {
                return _createTime;
            }
            set
            {
                if (value > SqlDateTime.MaxValue.Value)
                {
                    _createTime = SqlDateTime.MaxValue.Value;
                    return;
                }
                if (value < SqlDateTime.MinValue.Value)
                {
                    _createTime = SqlDateTime.MinValue.Value;
                    return;
                }
                _createTime = value;
            }
        }

        public int IsExpired { get; set; }

        public int UserID { get; set; }
    }
}
