using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RyanstaurantDataContract
{
    public class CashierEntity
    {
        //打印账单
        public CheckEntity PrintCheck(BillEntity bill)
        {
            throw new NotImplementedException();
        }

        //打印发票
        public void PrintVoice(CheckEntity check)
        {

        }

        //收钱找零
        public ChangeEntity RecievePayment(CheckEntity check, List<PaymentEntity> payments)
        {
            throw new NotImplementedException();
        }



    }
}
