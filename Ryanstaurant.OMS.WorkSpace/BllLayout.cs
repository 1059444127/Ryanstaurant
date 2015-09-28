using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Ryanstaurant.OMS.DataAccess;
using Ryanstaurant.OMS.DataContract;
using Ryanstaurant.OMS.IWorkSpace;

namespace Ryanstaurant.OMS.WorkSpace
{
    public class BllLayout:IBllLayout
    {
        public IList<Table> GetTables(IList<string> tableIdList)
        {
            using (var entity = new OmsEntity())
            {
                var tables = new List<Table>();
                if (!tableIdList.Any())
                    tables.AddRange(from t in entity.Tables where t.IsInUse
                        select new Table
                        {
                            ID = t.ID.ToString(),
                            No = t.DisplayNo,
                            Information = new TableInfo
                            {
                                CurrentStatus = (TableInfo.TableStatus)Enum.Parse(typeof(TableInfo.TableStatus), t.CurrentStatus.ToString(CultureInfo.InvariantCulture)),
                                Length=int.Parse(t.Length.ToString(CultureInfo.InvariantCulture)),
                                PosX = int.Parse(t.PosX.ToString(CultureInfo.InvariantCulture)),
                                PosY = int.Parse(t.PosY.ToString(CultureInfo.InvariantCulture)),
                                Width = int.Parse(t.Width.ToString(CultureInfo.InvariantCulture))
                            }
                        });
                else
                {
                    tables.AddRange(from t in entity.Tables
                                    where tableIdList.Contains(t.ID.ToString()) && t.IsInUse
                                    select new Table
                                    {
                                        ID = t.ID.ToString(),
                                        No = t.DisplayNo,
                                        Information = new TableInfo
                                        {
                                            CurrentStatus = (TableInfo.TableStatus)Enum.Parse(typeof(TableInfo.TableStatus), t.CurrentStatus.ToString(CultureInfo.InvariantCulture)),
                                            Length = int.Parse(t.Length.ToString(CultureInfo.InvariantCulture)),
                                            PosX = int.Parse(t.PosX.ToString(CultureInfo.InvariantCulture)),
                                            PosY = int.Parse(t.PosY.ToString(CultureInfo.InvariantCulture)),
                                            Width = int.Parse(t.Width.ToString(CultureInfo.InvariantCulture))
                                        }
                                    });
                }
                return tables;
            }
        }


        public IList<Table> SaveTables(IList<Table> tables)
        {
            using (var entity = new OmsEntity())
            {
                foreach (var table in tables)
                {
                    var tableInDb = (from t in entity.Tables
                        where string.Equals(table.ID, t.ID.ToString(), StringComparison.InvariantCultureIgnoreCase)
                        && t.IsInUse
                        select t).FirstOrDefault();

                    if (tableInDb == null)
                    {
                        entity.Tables.Add(new Tables
                        {
                            DisplayNo = table.No,
                            Length = table.Information.Length,
                            PosX = table.Information.PosX,
                            PosY = table.Information.PosY,
                            Width = table.Information.Width,
                            CurrentStatus = table.Information.CurrentStatus.ToInt()
                        });
                    }
                    else
                    {
                        tableInDb.DisplayNo = table.No;
                        tableInDb.Length = table.Information.Length;
                        tableInDb.PosX = table.Information.PosX;
                        tableInDb.PosY = table.Information.PosY;
                        tableInDb.Width = table.Information.Width;
                        tableInDb.CurrentStatus = table.Information.CurrentStatus.ToInt();
                    }
                }

                entity.SaveChanges();
                var idList = (from t in tables select t.ID).ToList();

                return GetTables(idList);

            }
        }


        public void RemoveTables(IList<string> tableIds)
        {
            using (var entity = new OmsEntity())
            {
                foreach (var tableId in tableIds)
                {
                    var tablesInDb = (from t in entity.Tables
                        where string.Equals(tableId, t.ID.ToString(), StringComparison.InvariantCultureIgnoreCase)
                        select t).FirstOrDefault();

                    if (tablesInDb == null) continue;
                    tablesInDb.Disabled = 1;
                }

                entity.SaveChanges();
            }
        }

        public Table CombineTable(IList<Table> tables, Table combineTable)
        {
            //验证状态，如果有已经合并或者拆分的桌子，则抛出异常，恢复需要使用UncombineTable

            using (var entity = new OmsEntity())
            {
                //添加新的合并桌
                var newTable = SaveTables(new List<Table>
                {
                    combineTable
                }).FirstOrDefault();

                if (newTable == null)
                    return null;

                combineTable.ID = newTable.ID;
                combineTable.Information = newTable.Information;
                combineTable.No = newTable.No;

                foreach (var table in tables)
                {
                    var tableInDb = (from t in entity.Tables
                        where string.Equals(t.ID.ToString(), table.ID, StringComparison.InvariantCultureIgnoreCase)
                              && t.IsInUse
                        select t).FirstOrDefault();

                    if (tableInDb == null)
                        continue;

                    tableInDb.CurrentStatus = TableInfo.TableStatus.Combined.ToInt();

                    entity.TableRelations.Add(new TableRelations
                    {
                        Disabled = 0,
                        TableID = Guid.Parse(combineTable.ID),
                        RelateTableID = tableInDb.ID
                    });
                }

                entity.SaveChanges();

                return combineTable;
            }
        }

        public IList<Table> SplitTables(Table table, List<Table> splitTables)
        {
            //验证状态，如果有已经合并或者拆分的桌子，则抛出异常，恢复需要使用UnsplitTable


            using (var entity = new OmsEntity())
            {
                //新增分开的桌子
                splitTables = SaveTables(splitTables).ToList();


                var tableInDb = (from t in entity.Tables
                    where string.Equals(t.ID.ToString(), table.ID, StringComparison.InvariantCultureIgnoreCase)
                          && t.IsInUse
                    select t).FirstOrDefault();


                if (tableInDb == null) return null;

                tableInDb.CurrentStatus = TableInfo.TableStatus.Splited.ToInt();

                foreach (var splitTable in splitTables)
                {
                    entity.TableRelations.Add(new TableRelations
                    {
                        Disabled = 0,
                        RelateTableID = Guid.Parse(splitTable.ID),
                        TableID = tableInDb.ID
                    });
                }


                entity.SaveChanges();
                return splitTables;

            }
        }




        public IList<Table> UnCombineTable(Table combinedTable)
        {
            throw new NotImplementedException();
        }

        public Table UnSpliTables(List<Table> splitedTables)
        {
            throw new NotImplementedException();
        }
    }
}
