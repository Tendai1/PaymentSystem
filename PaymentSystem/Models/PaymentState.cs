using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PaymentSystem.Models
{
    public class PaymentState
    {
        [Key]
        [ForeignKey("PaymentId")]
        public int PaymentId { get; set; }
        public Status State { get; set; }
        public enum Status
        {
            Pending,
            Processed,
            Failed
        }
    }
}