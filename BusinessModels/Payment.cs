using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessModels
{
    public class Payment
    {
        [Flags]
        public enum PaymentType
        {
            Cash,
            CreditCard,
            DebitCard,
            Coupon,
            Signature

        }




        private PaymentType _PayMode = PaymentType.Cash;

        public PaymentType PayMode
        {
            get { return _PayMode; }
            set { _PayMode = value; }
        }




        private double _Amount = 0;

        public double Amount
        {
            get { return _Amount; }
            set { _Amount = value; }
        }




    }
}
