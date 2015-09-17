using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ryanstaurant.UMS.DataAccess.EF;

namespace Ryanstaurant.UMS.WorkSpace
{
    public class BllBase
    {
        protected UmsEntities Entities;


        public BllBase()
        {
            Entities = new UmsEntities();
        }



        ~BllBase()
        {
            if (Entities!=null)
                Entities.Dispose();
        }




    }
}
