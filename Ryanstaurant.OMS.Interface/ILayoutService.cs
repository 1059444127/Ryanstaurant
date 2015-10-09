using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Net.Mail;
using System.ServiceModel;
using Ryanstaurant.OMS.DataContract;

namespace Ryanstaurant.OMS.Interface
{
    [ServiceContract]
    public interface ILayoutService
    {
        [OperationContract(Name = "GetAllTables")]
        IList<Table> GetTables();
        [OperationContract]
        IList<Table> GetTables(IList<string> tableIdList);

        [OperationContract]
        void SetTable(List<Table> tables);

        [OperationContract]
        void RemoveTable(List<string> tableId);

        [OperationContract]
        Table CombineTable(List<Table> tables,Table combineTable);

        [OperationContract]
        IList<Table> SplitTable(Table table, List<Table> splitTables);

        [OperationContract]
        IList<Table> UnCombineTable(Table combineTable);

        [OperationContract]
        Table UnSplitTable(Table splitedTable);

    }
}
