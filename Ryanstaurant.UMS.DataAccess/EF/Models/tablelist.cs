namespace Ryanstaurant.UMS.DataAccess.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("tablelist")]
    public partial class tablelist
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; }

        [StringLength(50)]
        public string DisplayNo { get; set; }

        public int? PosX { get; set; }

        public int? PosY { get; set; }

        public int? Width { get; set; }

        public int? Length { get; set; }
    }
}
