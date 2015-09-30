using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ryanstaurant.GMS.DataContract;

namespace Ryanstaurant.GMS.IWorkSpace
{
    public interface IBllSysConfig
    {


        string GetConfigValue(string shortCall);


        List<Config> GetConfigs(List<string> shortCalList);




    }
}
