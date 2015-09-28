namespace Ryanstaurant.OMS.DataAccess
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Derates
    {
        public Guid ID { get; set; }

        [StringLength(100)]
        public string Name { get; set; }

        [Column(TypeName = "money")]
        public decimal? Amount { get; set; }

        public int? DerateType { get; set; }

        public int? AimType { get; set; }

        [StringLength(250)]
        public string Description { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        [Column(TypeName = "money")]
        public decimal? DerateLine { get; set; }

        public int Disabled { get; set; }
    }
}
