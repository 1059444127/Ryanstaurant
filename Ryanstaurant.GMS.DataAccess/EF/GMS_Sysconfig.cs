using System.ComponentModel.DataAnnotations;

namespace Ryanstaurant.GMS.DataAccess.EF
{
    public partial class GMS_Sysconfig
    {
        [Key]
        [StringLength(200)]
        public string ShortCall { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        [StringLength(500)]
        public string ConfigValue { get; set; }
    }
}
