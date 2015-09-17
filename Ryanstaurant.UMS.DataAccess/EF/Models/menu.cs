namespace Ryanstaurant.UMS.DataAccess.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("menu")]
    public partial class menu
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int id { get; set; }

        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(100)]
        public string MType { get; set; }

        public int? DishID { get; set; }

        [StringLength(200)]
        public string Description { get; set; }

        [StringLength(500)]
        public string SubDescription { get; set; }

        public int? SuiteID { get; set; }
    }
}
