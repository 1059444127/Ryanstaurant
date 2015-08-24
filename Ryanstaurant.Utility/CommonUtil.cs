using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Linq;
using System.Text;

namespace Ryanstaurant.Utility
{
    public static class CommonUtil
    {
        public static void LoadTypeFromTable(object obj, DataTable dtdataTable)
        {
            var type = obj.GetType();
            var properties = type.GetProperties();
            foreach (var property in properties.Where(property => dtdataTable.Columns.Contains(property.Name)))
            {
                property.SetValue(obj, dtdataTable.Rows[0][property.Name], null);
            }
        }





    }
}
