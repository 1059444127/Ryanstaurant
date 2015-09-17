namespace Ryanstaurant.UMS.DataAccess.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("navigator")]
    public partial class navigator
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int id { get; set; }

        public int? ParentID { get; set; }

        [StringLength(100)]
        public string Label { get; set; }

        [StringLength(200)]
        public string ClassName { get; set; }

        public int? AuthorityID { get; set; }

        public int? SortNumber { get; set; }

        [StringLength(200)]
        public string PicURL { get; set; }
    }
}
