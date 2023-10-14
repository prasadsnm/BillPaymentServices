using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoicePaymentServices.Infra.DBEntities
{
    [Table("Account")]
    public class Account
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }

        // This is to simplify the demo code only.
        // Need to re-visit
        public bool AcceptBank { get; set; }
        public bool AcceptEmail { get; set; }
        public bool AcceptCreditCard { get; set; }
        public bool AcceptOther { get; set; }
    }
}
