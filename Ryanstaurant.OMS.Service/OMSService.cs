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
            return _menuService.GetMenus();
        }

        public Menu AddMenu(Menu menu)
        {
            return _menuService.AddMenu(menu);
        }

        public Menu ChangeMenu(Menu menu)
        {
            return _menuService.ChangeMenu(menu);
        }

        public Menu DisableMenu(Menu menu)
        {
            return _menuService.DisableMenu(menu);
        }

        public Menu EnableMenu(Menu menu)
        {
            return _menuService.EnableMenu(menu);
        }

        public void RemoveMenu(Menu menu)
        {
            _menuService.RemoveMenu(menu);
        }

        public Order CreateReservation(Order order)
        {
            return _orderService.CreateReservation(order);
        }

        public Order CreateOrder(Order order, IList<Item> items)
        {
            return _orderService.CreateOrder(order, items);
        }

        public Order QueryOrder(string orderId)
        {
            return _orderService.QueryOrder(orderId);
        }

        public void ChangeOrder(Order order, IList<Item> items)
        {
            _orderService.ChangeOrder(order, items);
        }

        public void AttachOrder(string orderId, IList<Item> items)
        {
            _orderService.AttachOrder(orderId, items);
        }

        public void PendingOrder(string orderId, string pendingReason)
        {
            _orderService.PendingOrder(orderId, pendingReason);
        }

        public void RevokeOrder(string orderId, string revokeReason)
        {
            _orderService.RevokeOrder(orderId, revokeReason);
        }

        public void CancelOrder(string orderId, string cancelReason)
        {
            _orderService.CancelOrder(orderId, cancelReason);
        }

        public Order CombineOrder(List<Order> orders)
        {
            return _orderService.CombineOrder(orders);
        }

        public IList<Order> SplitOrder(string orderId, IList<Order> orders)
        {
            return _orderService.SplitOrder(orderId, orders);
        }

        public Bill GetBill(Order order)
        {
            return _orderService.GetBill(order);
        }

        public Check PayCheck(Bill bill, List<Charge> charges)
        {
            return _orderService.PayCheck(bill, charges);
        }

        public IList<Order> QueryOrder(string startDateTime, string endDateTime)
        {
            return _orderService.QueryOrder(startDateTime, endDateTime);
        }
    }
}
