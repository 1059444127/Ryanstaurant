namespace Ryanstaurant.OMS.DataAccess
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Items
    {
        public Guid ID { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Column(TypeName = "money")]
        public decimal Price { get; set; }

        [Column(TypeName = "money")]
        public decimal Cost { get; set; }

        [StringLength(250)]
        public string Description { get; set; }

        [StringLength(Int32.MaxValue)]
        public string ChildIdList { get; set; }

        public int ItemType { get; set; }

        public int Disabled { get; set; }
    }
}
