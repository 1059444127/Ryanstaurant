using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ryanstaurant.OMS.DataContract;

namespace Ryanstaurant.OMS.IWorkSpace
{
    public interface IBllLayout
    {
        IList<Table> GetTables(IList<string> tableIdList);

        IList<Table> SaveTables(IList<Table> tables);

        void RemoveTables(IList<string> tableIds);


        Table CombineTable(IList<Table> tables, Table combineTable);


        IList<Table> SplitTables(Table table, List<Table> splitTables);


        IList<Table> UnCombineTable(Table combinedTable);

        Table UnSplitTables(Table splitedTable);


    }
}
