using System;
using System.Collections.Generic;
using Ryanstaurant.OMS.DataContract;
using Ryanstaurant.OMS.Interface;
using Ryanstaurant.OMS.WorkSpace;

namespace Ryanstaurant.OMS.Service
{
    public class OrderService:IOrderService
    {
        private readonly BllOrder bllOrder = new BllOrder();


        public Order CreateReservation(Order order)
        {
            return bllOrder.Reserve(order);
        }

        public Order CreateOrder(Order order, IList<Item> items)
        {
            return bllOrder.Add(order, items);
        }

        public Order QueryOrder(string orderId)
        {
            return bllOrder.Get(orderId);
        }

        public void ChangeOrder(Order order, IList<Item> items)
        {
            bllOrder.Save(order, items);
        }

        public void AttachOrder(string orderId, IList<Item> items)
        {
            bllOrder.Save(orderId, items);
        }

        public void PendingOrder(string orderId, string pendingReason)
        {
            bllOrder.Pending(orderId, pendingReason);
        }

        public void RevokeOrder(string orderId, string revokeReason)
        {
            bllOrder.Revoke(orderId, revokeReason);
        }

        public void CancelOrder(string orderId, string cancelReason)
        {
            bllOrder.Cancel(orderId, cancelReason);
        }

        public Order CombineOrder(List<Order> orders)
        {
            return bllOrder.Combine(orders);
        }

        public IList<Order> SplitOrder(string orderId, IList<Order> orders)
        {
            return bllOrder.Split(orderId, orders);
        }

        public Bill GetBill(Order order)
        {
            return bllOrder.GetBill(order);
        }

        public Check PayCheck(Bill bill, List<Charge> charges)
        {
            return bllOrder.Pay(bill, charges);
        }

        public IList<Order> QueryOrder(string startDateTime, string endDateTime)
        {
            return bllOrder.Get(startDateTime, endDateTime);
        }
    }
}
