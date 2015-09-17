namespace Ryanstaurant.UMS.DataAccess.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("tableimageconfig")]
    public partial class tableimageconfig
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Status { get; set; }

        [StringLength(200)]
        public string PicURL { get; set; }
    }
}
