using PaymentSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentSystem.GatewayInterfaces
{
    interface IPremiumPaymentService
    {
        bool isAvailable();
        PaymentState.Status processPayment(Payment paymentParams);
    }
}
