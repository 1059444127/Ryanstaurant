using System;
using System.Collections.Generic;
using System.Reflection;
using Ryanstaurant.OMS.Interface;
using Ryanstaurant.OMS.DataContract;

namespace Ryanstaurant.OMS.Service
{
    public class OMSService:IOmsService
    {
        private readonly ILayoutService _layoutService;
        private readonly IMenuService _menuService;
        private readonly IOrderService _orderService;


        public OMSService()
        {
            _layoutService = new LayoutService();
            _menuService=new MenuService();
            _orderService=new OrderService();
        }



        public Order Create(Order order, IList<Item> items)
        {
            throw new NotImplementedException();
        }

        public Order Query(string orderId)
        {
            throw new NotImplementedException();
        }

        public Order Change(Order order)
        {
            throw new NotImplementedException();
        }

        public Order Attach(string orderId, IList<Item> items)
        {
            throw new NotImplementedException();
        }

        public Order Revoke(string orderId)
        {
            throw new NotImplementedException();
        }

        public Order CombineOrder(List<Order> orders)
        {
            throw new NotImplementedException();
        }

        public IList<Order> SplitOrder(string orderId, IList<Order> orders)
        {
            throw new NotImplementedException();
        }

        public Bill GetBill(Order order)
        {
            throw new NotImplementedException();
        }

        public Check PayCheck(Bill bill, List<Charge> charges)
        {
            throw new NotImplementedException();
        }

        public IList<Order> Query(string startDateTime, string endDateTime)
        {
            throw new NotImplementedException();
        }

        public IList<Table> GetTables()
        {
            return _layoutService.GetTables();
        }

        public IList<Table> GetTables(IList<string> tableIdList)
        {
            return _layoutService.GetTables(tableIdList);
        }

        public void SetTable(List<Table> tables)
        {
            _layoutService.SetTable(tables);
        }

        public void RemoveTable(List<string> tableId)
        {
            _layoutService.RemoveTable(tableId);
        }

        public Table CombineTable(List<Table> tables, Table combineTable)
        {
            return _layoutService.CombineTable(tables, combineTable);
        }

        public IList<Table> SplitTable(Table table, List<Table> splitTables)
        {
            return _layoutService.SplitTable(table, splitTables);
        }

        public IList<Table> UnCombineTable(Table combineTable)
        {
            return _layoutService.UnCombineTable(combineTable);
        }

        public Table UnSplitTable(Table splitedTable)
        {
            return _layoutService.UnSplitTable(splitedTable);
        }

        public IList<Menu> GetMenus()
        {
            throw new NotImplementedException();
        }

        public Menu AddMenu(Menu menu)
        {
            throw new NotImplementedException();
        }

        public Menu ChangeMenu(Menu menu)
        {
            throw new NotImplementedException();
        }

        public Menu DisableMenu(Menu menu)
        {
            throw new NotImplementedException();
        }

        public Menu EnableMenu(Menu menu)
        {
            throw new NotImplementedException();
        }

        public void RemoveMenu(Menu menu)
        {
            throw new NotImplementedException();
        }
    }
}
