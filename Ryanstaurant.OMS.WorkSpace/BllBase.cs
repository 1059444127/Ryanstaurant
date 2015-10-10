using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ryanstaurant.OMS.DataAccess;

namespace Ryanstaurant.OMS.WorkSpace
{
    public class BllBase
    {
        
        protected OmsEntity Entity = new OmsEntity();

        public BllBase() { }


        public BllBase(OmsEntity omsEntity)
        {
            Entity = omsEntity;
        }




    }
}
