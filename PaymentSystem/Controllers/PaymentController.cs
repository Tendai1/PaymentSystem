using PaymentSystem.DataContext;
using PaymentSystem.Models;
using PaymentSystem.PaymentGateWays;
using PaymentSystem.ServiceEntities;
using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Web.Http;
using System.Web.Http.Description;
using FromBodyAttribute = System.Web.Http.FromBodyAttribute;
using RouteAttribute = System.Web.Http.RouteAttribute;

namespace PaymentSystem.Controllers
{
    public class PaymentController : ApiController
    {
        public object ICheapGateway { get; private set; }

        [Route("api/makepayment")]
        [ResponseType(typeof(ResponseEntity))]
        public HttpResponseMessage Post([FromBody] Payment paymentParameters)
        {
            HttpResponseMessage response = null;
            try
            {
                using (var context = new PaymentDBcontext())
                {
                    if (paymentParameters != null )
                    {
                        //basic user input validation
                        if(!string.IsNullOrEmpty(paymentParameters.SecurityCode) && paymentParameters.SecurityCode.Trim().Length > 3)
                        {
                            throw new Exception("Security code should only be 3 digits");
                        }
                        var pmt = paymentParameters;

                        context.Payment.Add(pmt);
                        context.SaveChanges();

                        CheapPaymentGateway cheap = new CheapPaymentGateway();
                        ExpensivePaymentGateway expensive = new ExpensivePaymentGateway();
                        PremiumPaymentService premium = new PremiumPaymentService();

                        if(pmt.Amount <= 20)
                        {
                            pmt.Status.State = cheap.processPayment(pmt);
                        }
                        else if(pmt.Amount <= 500)
                        {
                            if(expensive.isAvailable())
                            {
                                pmt.Status.State = expensive.processPayment(pmt);
                            }
                            else
                            {
                                pmt.Status.State = cheap.processPayment(pmt);
                            }
                        }
                        else
                        {
                            int retry = 0;

                            while (retry < 3 && pmt.Status.State != PaymentState.Status.Processed)
                            {
                                pmt.Status.State = premium.processPayment(pmt);
                                retry++;
                                Thread.Sleep(3000);
                            }
                        }

                        context.Payment.Update(pmt);
                        context.SaveChanges();

                        response = Request.CreateResponse(pmt.Status.State == PaymentState.Status.Processed ? HttpStatusCode.OK : HttpStatusCode.BadRequest, pmt);
                        
                        return response;
                    }
                    else
                    {
                        response = Request.CreateResponse(HttpStatusCode.BadRequest, "Error");
                        return response;
                    }
                }
            }
            catch(Exception ex)
            {
                response = Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
                return response;
            }
        }
    }
}
