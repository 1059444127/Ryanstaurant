using System;
using System.Collections.Generic;
using System.Linq;
using Ryanstaurant.OMS.DataAccess;
using Ryanstaurant.OMS.DataContract;

namespace Ryanstaurant.OMS.WorkSpace
{
    public class BllLayout:BllBase
    {

        public BllLayout()
        {
            
        }


        public BllLayout(OmsEntity entity):base(entity)
        {
        }




        public IList<Table> GetTables(IList<string> tableIdList)
        {
            var tables = new List<Table>();
            if (tableIdList == null || !tableIdList.Any())
            {
                tables.AddRange((from t in Entity.OMS_Tables
                    where t.Disabled == 0 && (t.CurrentStatus == 0 || t.CurrentStatus == 1 || t.CurrentStatus == 2)
                    select t).ToList().ConvertAll(Table.ConvertFromEntity));
            }
            else
            {
                tables.AddRange((from t in Entity.OMS_Tables
                    where
                        tableIdList.Contains(t.ID.ToString()) &&
                        (t.Disabled == 0 && (t.CurrentStatus == 0 || t.CurrentStatus == 1 || t.CurrentStatus == 2))
                    select t).ToList().ConvertAll(Table.ConvertFromEntity));
            }
            return tables;

        }


        public IList<Table> SaveTables(IList<Table> tables)
        {
            foreach (var table in tables)
            {
                var tableInDb = (from t in Entity.OMS_Tables
                    where string.Equals(table.ID, t.ID.ToString(), StringComparison.InvariantCultureIgnoreCase)
                          && t.Disabled == 0 && (t.CurrentStatus == 0 || t.CurrentStatus == 1 || t.CurrentStatus == 2)
                    select t).FirstOrDefault();

                if (tableInDb == null)
                {
                    Entity.OMS_Tables.Add(table.ToEntity<OMS_Tables>());
                }
                else
                {
                    tableInDb.DisplayNo = table.No;
                    tableInDb.Length = table.Length;
                    tableInDb.PosX = table.PosX;
                    tableInDb.PosY = table.PosY;
                    tableInDb.Width = table.Width;
                    tableInDb.CurrentStatus = table.CurrentStatus;
                }
            }

            Entity.SaveChanges();
            var idList = (from t in tables select t.ID.ToUpper()).ToList();
            var returnTables = ((from t in Entity.OMS_Tables
                                where t.Disabled == 0 && (t.CurrentStatus == 0 || t.CurrentStatus == 1 || t.CurrentStatus == 2)
                      && idList.Contains(t.ID.ToString().ToUpper())
                                 select t).ToList().ConvertAll(Table.ConvertFromEntity)).ToList();

            return returnTables;

        }


        public void RemoveTables(IList<string> tableIds)
        {
            foreach (var tableId in tableIds)
            {
                var tablesInDb = (from t in Entity.OMS_Tables
                    where string.Equals(tableId, t.ID.ToString(), StringComparison.InvariantCultureIgnoreCase)
                    select t).FirstOrDefault();

                if (tablesInDb == null) continue;
                tablesInDb.Disabled = 1;
            }

            Entity.SaveChanges();

        }

        public Table CombineTable(IList<Table> tables, Table combineTable)
        {
            //验证状态，如果有已经合并或者拆分的桌子，则抛出异常，恢复需要使用UncombineTable

            //添加新的合并桌
            var newTable = Entity.OMS_Tables.Add(combineTable.ToEntity<OMS_Tables>());

            if (newTable != null)
                combineTable.ID = newTable.ToString();

            foreach (var table in tables)
            {
                var tableInDb = (from t in Entity.OMS_Tables
                    where string.Equals(t.ID.ToString(), table.ID, StringComparison.InvariantCultureIgnoreCase)
                          && (t.Disabled == 0 && (t.CurrentStatus == 0 || t.CurrentStatus == 1 || t.CurrentStatus == 2))
                    select t).FirstOrDefault();

                if (tableInDb == null)
                    continue;

                tableInDb.CurrentStatus = (int)Table.TableStatus.Combined;

                Entity.OMS_TableRelations.Add(new OMS_TableRelations
                {
                    Disabled = 0,
                    TableID = Guid.Parse(combineTable.ID),
                    RelateTableID = tableInDb.ID
                });
            }

            Entity.SaveChanges();

            return combineTable;

        }

        public IList<Table> SplitTables(Table table, List<Table> splitTables)
        {
            //验证状态，如果有已经合并或者拆分的桌子，则抛出异常，恢复需要使用UnsplitTable


            //新增分开的桌子
            splitTables = SaveTables(splitTables).ToList();


            var tableInDb = (from t in Entity.OMS_Tables
                where string.Equals(t.ID.ToString(), table.ID, StringComparison.InvariantCultureIgnoreCase)
                      && (t.Disabled == 0 && (t.CurrentStatus == 0 || t.CurrentStatus == 1 || t.CurrentStatus == 2))
                select t).FirstOrDefault();


            if (tableInDb == null) return null;

            tableInDb.CurrentStatus = (int)Table.TableStatus.Splited;

            foreach (var splitTable in splitTables)
            {
                Entity.OMS_TableRelations.Add(new OMS_TableRelations
                {
                    Disabled = 0,
                    RelateTableID = Guid.Parse(splitTable.ID),
                    TableID = tableInDb.ID
                });
            }


            Entity.SaveChanges();
            return splitTables;


        }




        public IList<Table> UnCombineTable(Table combinedTable)
        {
            var orgTables = new List<Table>();

            var combineTableIds = (from tr in Entity.OMS_TableRelations
                where
                    tr.Disabled == 0 &&
                    string.Equals(tr.TableID.ToString(), combinedTable.ID,
                        StringComparison.InvariantCultureIgnoreCase)
                select tr.RelateTableID).ToList();

            foreach (var combineTableId in combineTableIds)
            {
                var orgTable = (from t in Entity.OMS_Tables
                    where t.CurrentStatus == (int)Table.TableStatus.Combined
                          && t.ID == combineTableId
                    select t).FirstOrDefault();

                if (orgTable == null)
                    continue;

                orgTable.CurrentStatus = (int)Table.TableStatus.Spare;

                orgTables.Add(Table.ConvertFromEntity(orgTable));
            }

            var tableInDb = Entity.OMS_Tables.Find(Guid.Parse(combinedTable.ID));
            tableInDb.Disabled = 1;

            Entity.SaveChanges();
            return orgTables;


        }

        public Table UnSplitTables(Table splitedTable)
        {
            var splitTableRelation = (from tr in Entity.OMS_TableRelations
                where tr.Disabled == 0
                      &&
                      string.Equals(tr.RelateTableID.ToString(), splitedTable.ID,
                          StringComparison.InvariantCultureIgnoreCase)
                select new
                {
                    tr.TableID,
                    tr.RelateTableID
                }).ToList();


            var orgTable = Entity.OMS_Tables.Find(splitTableRelation[0].TableID);
            orgTable.CurrentStatus = (int)Table.TableStatus.Spare;

            foreach (var tr in splitTableRelation)
            {
                Entity.OMS_Tables.Find(tr.RelateTableID).Disabled = 1;
            }

            Entity.SaveChanges();

            return Table.ConvertFromEntity(orgTable);

        }
    }
}
