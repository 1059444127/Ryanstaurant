namespace Ryanstaurant.OMS.DataAccess
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("MenuItem")]
    public partial class MenuItem
    {
        [Key]
        [Column(Order = 0)]
        public Guid MenuID { get; set; }

        [Key]
        [Column(Order = 1)]
        public Guid ItemID { get; set; }
    }
}
