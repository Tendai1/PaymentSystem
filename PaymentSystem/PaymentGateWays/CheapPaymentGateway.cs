using PaymentSystem.GatewayInterfaces;
using PaymentSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PaymentSystem.PaymentGateWays
{
    public class CheapPaymentGateway : ExpensivePaymentGateway, ICheapPaymentGateway
    {
        
    }
}