using System.Collections.Generic;
using Ryanstaurant.OMS.DataContract;

namespace Ryanstaurant.OMS.Interface
{
    public interface ILayoutService
    {
        IList<Table> GetTables();
        IList<Table> GetTables(IList<string> tableIdList);

        Table PutTable(Table table);
        Table DisableTable(string tableId);
        Table EnableTable(string tableId);
        void RemoveTable(string tableId);

        Table CombineTable(List<Table> tables);
        List<Table> SplitTable(Table table);

    }
}
