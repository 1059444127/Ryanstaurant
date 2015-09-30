namespace Ryanstaurant.OMS.DataAccess
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Orders
    {
        public Guid ID { get; set; }

        public Guid DiscountID { get; set; }

        public Guid DerateID { get; set; }

        [Column(TypeName = "money")]
        public decimal SubTotal { get; set; }

        [Column(TypeName = "money")]
        public decimal Total { get; set; }

        public Guid ChargeID { get; set; }

        public Guid ChangeID { get; set; }

        public string PendingReason { get; set; }

        public string RevokeReason { get; set; }

        public string CancelReason { get; set; }

        public Guid TableId { get; set; }

        public int Status { get; set; }

        public int IsReserved { get; set; }

        public int Disabled { get; set; }
    }
}
