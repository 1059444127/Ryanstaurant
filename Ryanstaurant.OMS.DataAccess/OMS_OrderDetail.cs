namespace Ryanstaurant.OMS.DataAccess
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class OMS_OrderDetail
    {
        [Key]
        [Column(Order = 0)]
        public Guid OrderID { get; set; }

        [Key]
        [Column(Order = 1)]
        public Guid ItemID { get; set; }

        [Column(TypeName = "money")]
        public decimal OraginPrice { get; set; }

        public Guid? DiscountID { get; set; }

        public Guid? DerateID { get; set; }

        [Column(TypeName = "money")]
        public decimal Price { get; set; }

        public int Disabled { get; set; }

        public int SpecialRequest { get; set; }

        public decimal Quantity { get; set; }
    }
}
