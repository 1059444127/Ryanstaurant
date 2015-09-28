namespace Ryanstaurant.OMS.DataAccess
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ItemDerates
    {
        [Key]
        [Column(Order = 0)]
        public Guid ItemID { get; set; }

        [Key]
        [Column(Order = 1)]
        public Guid DerateID { get; set; }

        public int DerateLevel { get; set; }

        public int Disabled { get; set; }

        public int Prior { get; set; }
    }
}
