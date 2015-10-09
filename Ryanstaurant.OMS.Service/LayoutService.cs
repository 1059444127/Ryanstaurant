using System;
using System.Collections.Generic;
using System.Reflection;
using Ryanstaurant.OMS.Interface;
using Ryanstaurant.OMS.DataContract;
using Ryanstaurant.OMS.WorkSpace;

namespace Ryanstaurant.OMS.Service
{
    public class LayoutService:ILayoutService
    {
        protected BllLayout BllLayout;


        public LayoutService()
        {
            BllLayout = new BllLayout();
        }



        public IList<Table> GetTables()
        {
            return BllLayout.GetTables(null);
        }

        public IList<Table> GetTables(IList<string> tableIdList)
        {
            return BllLayout.GetTables(tableIdList);
        }

        public void SetTable(List<Table> tables)
        {
            BllLayout.SaveTables(tables);
        }

        public void RemoveTable(List<string> tableId)
        {
            BllLayout.RemoveTables(tableId);
        }

        public Table CombineTable(List<Table> tables, Table combineTable)
        {
            return BllLayout.CombineTable(tables, combineTable);
        }

        public IList<Table> SplitTable(Table table, List<Table> splitTables)
        {
            return BllLayout.SplitTables(table, splitTables);
        }

        public IList<Table> UnCombineTable(Table combineTable)
        {
            return BllLayout.UnCombineTable(combineTable);
        }

        public Table UnSplitTable(Table splitedTable)
        {
            return BllLayout.UnSplitTables(splitedTable);
        }
    }
}
