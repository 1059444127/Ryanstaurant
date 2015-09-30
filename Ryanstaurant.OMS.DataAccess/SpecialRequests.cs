using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Ryanstaurant.OMS.DataAccess
{
    public partial class SpecialRequests
    {

        public int ID { get; set; }

        [Required]
        public string Name { get; set; }


        [Required]
        public string RequestGroup { get; set; }

        [Required]
        public int Disabled { get; set; }

    }
}
