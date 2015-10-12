using System;
using System.Collections.Generic;
using Ryanstaurant.OMS.DataContract;

namespace Ryanstaurant.OMS.WorkSpace
{
    public class BllOrder:BllBase
    {

        public Order AddOrder(Order order, IList<Item> items)
        {
            throw new NotImplementedException();
        }

        public Order ReserveOrder(Order order)
        {
            throw new NotImplementedException();
        }

        public Order Find(string orderId)
        {
            throw new NotImplementedException();
        }

        public Order GetOrder(string orderId)
        {
            throw new NotImplementedException();
        }

        public object SaveOrder(Order order, IList<Item> items)
        {
            throw new NotImplementedException();
        }

        public void SaveOrder(string orderId, IList<Item> items)
        {
            throw new NotImplementedException();
        }

        public void Pending(string orderId, string pendingReason)
        {
            throw new NotImplementedException();
        }

        public Order Reserve(Order order)
        {
            throw new NotImplementedException();
        }

        public Order Add(Order order, IList<Item> items)
        {
            throw new NotImplementedException();
        }

        public Order Get(string orderId)
        {
            throw new NotImplementedException();
        }

        public void Save(Order order, IList<Item> items)
        {
            throw new NotImplementedException();
        }

        public void Save(string orderId, IList<Item> items)
        {
            throw new NotImplementedException();
        }

        public void Revoke(string orderId, string revokeReason)
        {
            throw new NotImplementedException();
        }

        public void Cancel(string orderId, string cancelReason)
        {
            throw new NotImplementedException();
        }

        public Order Combine(List<Order> orders)
        {
            throw new NotImplementedException();
        }

        public IList<Order> Split(string orderId, IList<Order> orders)
        {
            throw new NotImplementedException();
        }

        public Bill GetBill(Order order)
        {
            throw new NotImplementedException();
        }

        public Check Pay(Bill bill, List<Charge> charges)
        {
            throw new NotImplementedException();
        }

        public IList<Order> Get(string startDateTime, string endDateTime)
        {
            throw new NotImplementedException();
        }
    }
}
