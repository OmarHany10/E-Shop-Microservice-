using System;
using System.Collections.Generic;
using System.Text;

namespace Ordering.Domain.ValueObjects
{
    public class Payment
    {
        public string CardName { get; }
        public string CardNumber { get; }
        public string Expiration { get; }
        public string CVV { get; }
        public int PaymentMethod { get; }


        protected Payment()
        {
        }

        private Payment(string cardName, string cardNumber, string expiration, string cvv, int paymentMethod)
        {
            CardName = cardName;
            CardNumber = cardNumber;
            Expiration = expiration;
            CVV = cvv;
            PaymentMethod = paymentMethod;
        }

    }
}
