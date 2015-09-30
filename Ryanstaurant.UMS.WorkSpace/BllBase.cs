using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ryanstaurant.UMS.DataAccess.EF;

namespace Ryanstaurant.UMS.WorkSpace
{
    public class BllBase
    {
        protected UmsEntity Entity;


        public BllBase()
        {
            Entity = new UmsEntity();
        }



        ~BllBase()
        {
            if (Entity!=null)
                Entity.Dispose();
        }




    }
}
