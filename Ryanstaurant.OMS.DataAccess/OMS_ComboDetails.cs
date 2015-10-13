namespace Ryanstaurant.OMS.DataAccess
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class OMS_ComboDetails
    {
        [Key]
        [Column(Order = 0)]
        public Guid ComboId { get; set; }

        [Key]
        [Column(Order = 1)]
        public Guid ItemId { get; set; }
    }
}
