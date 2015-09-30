namespace Ryanstaurant.GMS.DataAccess.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class GMS_TokenLocation
    {
        [Key]
        [StringLength(50)]
        public string SystemName { get; set; }

        [StringLength(250)]
        public string ConnectionString { get; set; }


        public int IsLoginSystem { get; set; }
    }
}
