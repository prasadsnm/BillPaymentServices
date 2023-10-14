using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoicePaymentServices.Core.Models
{
    public class Account
    {
        public Guid Id { get; set; }

        [StringLength(300)]
        public string? Description { get; set; }

        [StringLength(150)]
        public string? Address {  get; set; }
        
        // This is to simplify the demo code only.
        // Need to re-visit
        public bool AcceptBank{  get; set; }
        public bool AcceptEmail { get; set; }
        public bool AcceptCreditCard { get; set; }
        public bool AcceptOther { get; set; }
    }
}
