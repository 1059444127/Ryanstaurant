using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ryanstaurant.GMS.DataAccess.EF;

namespace Ryanstaurant.GMS.WorkSpace
{
    public class BllBase
    {
        protected GmsEntity Entity;

        public BllBase(GmsEntity entity)
        {
            Entity = entity;
        }


        ~BllBase()
        {
            if (Entity == null) return;

            Entity.Dispose();
        }
    }
}
