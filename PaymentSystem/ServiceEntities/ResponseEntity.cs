using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PaymentSystem.ServiceEntities
{
    public class ResponseEntity
    {
        public int responseCode { get; set; }
        public string responseMessage { get; set; }
    }
}