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
        // here we assume this is to set up a future payment.
        // This table is only for this information and past successful payment
        public int Id { get; set; }
        public int InvoiceId { get; set; }

        // This should be easily retrived from front end.
        // For convenience only to save a db query.
        // Need re-visit
        public decimal InvoiceAmount { get; set; }

        // This should be easily retrived from front end.
        // For convenience only to save a db query.
        // Need re-visit
        public Guid BillToId { get; set; }
        public decimal PayAmount { get; set; }
        public DateTime PayDate { get; set; }
        public string Status { get; set; }
    }
}
