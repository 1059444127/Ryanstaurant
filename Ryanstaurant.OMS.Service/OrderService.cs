using System;
using System.Collections.Generic;
using Ryanstaurant.OMS.DataContract;
using Ryanstaurant.OMS.Interface;

namespace Ryanstaurant.OMS.Service
{
    public class OrderService:IOrderService
    {
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
    }
}
