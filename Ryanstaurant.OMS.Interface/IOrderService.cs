using System.Collections.Generic;
using Ryanstaurant.OMS.DataContract;

namespace Ryanstaurant.OMS.Interface
{
    public interface IOrderService
    {
        Order Create(Order order, IList<Item> items);

        Order Query(string orderId);

        Order Change(Order order);

        Order Attach(string orderId, IList<Item> items);

        Order Revoke(string orderId);

        Order CombineOrder(List<Order> orders);

        IList<Order> SplitOrder(string orderId, IList<Order> orders);

        Bill GetBill(Order order);

        Check GetCheck(Bill bill);

        Change PayCheck(Check check);

        IList<Order> Query(string startDateTime, string endDateTime);

    }
}
