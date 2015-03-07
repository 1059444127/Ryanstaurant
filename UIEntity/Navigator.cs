using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UIEntity
{
    public class Navigator
    {

        public string Label { get; set; }

        public int ID { get; set; }

        public int AuthorityID { get; set; }

        public List<Navigator> Child { get; set; }
    }
}
