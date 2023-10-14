using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoicePaymentServices.Core.Models
{
    public class Payment
    {
        // Not clear about the requirement and business logic behind,
        // So here we assume this is this is to set up a future payment.
        // This table is only for this information and past successful payment
        public int Id { get; set; }
        public Guid InvoiceId { get; set; }
        public decimal PayAmount {  get; set; }
        public DateTime PayDate { get; set; }
        public string Status { get; set; }
    }
}
