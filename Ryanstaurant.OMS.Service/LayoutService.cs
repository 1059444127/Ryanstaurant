using System;
using System.Collections.Generic;
using Ryanstaurant.OMS.Interface;
using Ryanstaurant.OMS.DataContract;

namespace Ryanstaurant.OMS.Service
{
    public class LayoutService:ILayoutService
    {

        public IList<Table> GetTables()
        {
            throw new NotImplementedException();
        }

        public IList<Table> GetTables(IList<string> tableIdList)
        {
            throw new NotImplementedException();
        }

        public void SetTable(List<Table> tables)
        {
            throw new NotImplementedException();
        }

        public void RemoveTable(List<string> tableId)
        {
            throw new NotImplementedException();
        }

        public Table CombineTable(List<Table> tables, Table combineTable)
        {
            throw new NotImplementedException();
        }

        public List<Table> SplitTable(Table table, List<Table> splitTables)
        {
            throw new NotImplementedException();
        }

        public IList<Table> UnCombineTable(Table combineTable)
        {
            throw new NotImplementedException();
        }

        public Table UnSplitTable(List<Table> splitedTable)
        {
            throw new NotImplementedException();
        }


        public Table UnSplitTable(Table splitedTable)
        {
            throw new NotImplementedException();
        }
    }
}
