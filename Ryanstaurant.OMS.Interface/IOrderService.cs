using System.Collections.Generic;
using System.ServiceModel;
using Ryanstaurant.OMS.DataContract;

namespace Ryanstaurant.OMS.Interface
{
    [ServiceContract]
    public interface IOrderService
    {
        [OperationContract]
        Order CreateReservation(Order order);
        [OperationContract]
        Order CreateOrder(Order order, IList<Item> items);
        [OperationContract(Name = "QueryOrderById")]
        Order QueryOrder(string orderId);
        [OperationContract]
        void ChangeOrder(Order order, IList<Item> items);
        [OperationContract]
        void AttachOrder(string orderId, IList<Item> items);
        [OperationContract]
        void PendingOrder(string orderId, string pendingReason);
        [OperationContract]
        void RevokeOrder(string orderId, string revokeReason);
        [OperationContract]
        void CancelOrder(string orderId, string cancelReason);
        [OperationContract]
        Order CombineOrder(List<Order> orders);
        [OperationContract]
        IList<Order> SplitOrder(string orderId, IList<Order> orders);
        [OperationContract]
        Bill GetBill(Order order);
        [OperationContract]
        Check PayCheck(Bill bill, List<Charge> charges);
        [OperationContract(Name = "QueryOrderByTime")]
        IList<Order> QueryOrder(string startDateTime, string endDateTime);

    }
}
