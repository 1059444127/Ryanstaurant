namespace Ryanstaurant.UMS.DataAccess.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Dishes
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; }

        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(10)]
        public string ShortCall { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        public decimal? Price { get; set; }

        public decimal? Cost { get; set; }

        [StringLength(20)]
        public string MainType { get; set; }

        [StringLength(20)]
        public string SubType { get; set; }

        [Column(TypeName = "image")]
        public byte[] Photo { get; set; }
    }
}
