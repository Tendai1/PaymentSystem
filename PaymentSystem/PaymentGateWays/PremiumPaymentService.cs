using PaymentSystem.GatewayInterfaces;
using PaymentSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PaymentSystem.PaymentGateWays
{
    public class PremiumPaymentService : IPremiumPaymentService
    {
        public bool isAvailable()
        {
            Random r = new Random();

            return new bool[] { true,false}[r.Next(2)];
        }
        public PaymentState.Status processPayment(Payment paymentParams)
        {
            bool cardDetailsValid = true;
            //validate card number length
            if (!CCNLuhnCheck(paymentParams.CreditCardNumber.Trim()))
            {
                cardDetailsValid = false;
            }

            //if security code is provided it should have max length of 3
            if (!string.IsNullOrEmpty(paymentParams.SecurityCode.Trim()) && paymentParams.SecurityCode.Trim().Length != 3)
            {
                cardDetailsValid = false;
            }

            //field is mandatory
            if (string.IsNullOrEmpty(paymentParams.CardHolder.Trim()))
            {
                cardDetailsValid = false;
            }

            //exp date cannot be in the past
            if (paymentParams.ExpirationDate < DateTime.Now.Date)
            {
                cardDetailsValid = false;
            }

            //mandatory and needs to be positive
            if (!(paymentParams.Amount > 0))
            {
                cardDetailsValid = false;
            }


            return cardDetailsValid ? PaymentState.Status.Processed : PaymentState.Status.Failed;
        }
        public bool CCNLuhnCheck(string CCN)
        {
            if (!string.IsNullOrEmpty(CCN) && new int[] { 14, 15, 16 }.Contains(CCN.Trim().Length))
            {
                return CCN.All(char.IsDigit) && CCN.Reverse()
                    .Select(c => (int) c )
                    .Select((thisNum, i) => i % 2 == 0
                        ? thisNum
                        : ((thisNum *= 2) > 9 ? thisNum - 9 : thisNum)
                    ).Sum() % 10 == 0;
            }
            else
            {
                return false;
            }
        }
    }
}