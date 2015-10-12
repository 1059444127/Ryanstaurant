namespace Ryanstaurant.OMS.DataAccess
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class OMS_Orders
    {
        public Guid ID { get; set; }

        [Column(TypeName = "money")]
        public decimal SubTotal { get; set; }

        [Column(TypeName = "money")]
        public decimal Total { get; set; }

        public Guid TableID { get; set; }

        public int IsReserved { get; set; }

        public int Status { get; set; }

        [StringLength(250)]
        public string PendingReason { get; set; }

        [StringLength(250)]
        public string RevokeReason { get; set; }

        [StringLength(250)]
        public string CancelReason { get; set; }

        public int Disabled { get; set; }
    }
}
