using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PaymentSystem.Models
{
    public class Payment
    {
        public Payment()
        {
            Status = new PaymentState { State = PaymentState.Status.Pending };
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int PaymentId { get; set; }
        [Required]
        [MaxLength(16, ErrorMessage = "CCN cannot exceed 16 characters")]
        public  string CreditCardNumber { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = "Card holder name cannot exceed 100 characters")]
        public string CardHolder { get; set; }

        [Required]
        public DateTime ExpirationDate { get; set; }

        [MaxLength(3, ErrorMessage = "Security code must be 3 digits")]
        public string SecurityCode { get; set; }

        [Required]
        public double Amount { get; set; }

        public PaymentState Status { get; set; } 

    }
}