using System.Collections.Generic;
using System.ServiceModel;
using Ryanstaurant.OMS.DataContract;

namespace Ryanstaurant.OMS.Interface
{
    [ServiceContract]
    public interface IOrderService
    {
        [OperationContract]
        Order Create(Order order, IList<Item> items);
        [OperationContract]
        Order Query(string orderId);
        [OperationContract]
        Order Change(Order order);
        [OperationContract]
        Order Attach(string orderId, IList<Item> items);
        [OperationContract]
        Order Revoke(string orderId);
        [OperationContract]
        Order CombineOrder(List<Order> orders);
        [OperationContract]
        IList<Order> SplitOrder(string orderId, IList<Order> orders);
        [OperationContract]
        Bill GetBill(Order order);
        [OperationContract]
        Check PayCheck(Bill bill, List<Charge> charges);
        [OperationContract]
        IList<Order> Query(string startDateTime, string endDateTime);

    }
}
