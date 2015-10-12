namespace Ryanstaurant.OMS.DataAccess
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class OMS_Discounts
    {
        public Guid ID { get; set; }

        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(250)]
        public string Description { get; set; }

        public decimal? Volumn { get; set; }

        [Column(TypeName = "money")]
        public decimal? DiscountLine { get; set; }

        public int? DiscountType { get; set; }
    }
}
