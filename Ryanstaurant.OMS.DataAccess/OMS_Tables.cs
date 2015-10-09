namespace Ryanstaurant.OMS.DataAccess
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class OMS_Tables
    {
        public Guid ID { get; set; }

        [Required]
        [StringLength(50)]
        public string DisplayNo { get; set; }

        public int PosX { get; set; }

        public int PosY { get; set; }

        public int Width { get; set; }

        public int Length { get; set; }

        public int CurrentStatus { get; set; }

        public int Disabled { get; set; }
    }
}
