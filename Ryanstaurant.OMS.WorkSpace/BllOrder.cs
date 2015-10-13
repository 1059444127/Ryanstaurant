using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Ryanstaurant.OMS.DataAccess;
using Ryanstaurant.OMS.DataContract;

namespace Ryanstaurant.OMS.WorkSpace
{
    public class BllOrder:BllBase
    {
        public Order Add(Order order, IList<Item> items)
        {
            Guid orderId;
            if (!Guid.TryParse(order.ID, out orderId))
            {
                orderId = Guid.NewGuid();
            }


            var orderInDb = Entity.OMS_Orders.Find(orderId);

            if (orderInDb!=null)
                throw new DuplicateNameException("已经存在ID为["+order.ID+"]的订单，请使用Attach进行添加");

            //订单主表添加
            Entity.OMS_Orders.Add(new OMS_Orders
            {
                Disabled=0,
                ID = orderId,
                IsReserved = 0,
                Status = (int)Order.OrderStatus.Ordering,
                TableID = Guid.Parse(order.TableId)
            });

            //添加订单明细
            Entity.OMS_OrderDetail.AddRange((from i in items
                select new OMS_OrderDetail
                {
                    ItemID = Guid.Parse(i.ID),
                    OrderID = orderId,
                    Price = decimal.Parse(i.Price),
                    Quantity = decimal.Parse(i.Quantity),
                    Status = (int) Item.OrderItemStatus.Normal
                }).ToList());


            Entity.SaveChanges();

            order.ID = orderId.ToString();

            return order;


        }

        public Order Get(string orderId)
        {
            //订单基本信息
            var orderInDb = Entity.OMS_Orders.Find(Guid.Parse(orderId));
            //订单相关项目信息


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

        public void Pending(string orderId, string pendingReason)
        {
            throw new NotImplementedException();
        }

        public Order Reserve(Order order)
        {
            throw new NotImplementedException();
        }
    }
}
