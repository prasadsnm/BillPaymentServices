using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoicePaymentServices.Infra.DBEntities
{
    [Table("Account")]
    public class Account
    {
        [Key]
        public Guid Id { get; set; }

        [DataType(DataType.Text)]
        public string Description { get; set; }

        [DataType(DataType.Text)]
        public string Address { get; set; }

        // This is to simplify the demo code only.
        // Need to re-visit
        [DataType(DataType.Text)]
        public string Bank { get; set; }

        [DataType(DataType.Text)]
        public string Email { get; set; }

        [DataType(DataType.Text)]
        public string CreditCard { get; set; }
    }
}
