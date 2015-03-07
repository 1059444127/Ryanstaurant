using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessModels
{
    public class Cashier
    {
        //打印账单
        public Check PrintCheck(Bill bill)
        {
            throw new NotImplementedException();
        }

        //打印发票
        public void PrintVoice(Check check)
        {

        }

        //收钱找零
        public Change RecievePayment(Check check,List<Payment> payments)
        {
            throw new NotImplementedException();
        }



    }
}
