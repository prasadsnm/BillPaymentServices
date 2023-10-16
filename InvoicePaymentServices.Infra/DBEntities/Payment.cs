using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoicePaymentServices.Infra.DBEntities
{
    [Table("Payment")]
    public class Payment
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        [ForeignKey("Invoice")]
        public int InvoiceId { get; set; }

        // This is a convenience way to get payment by account id.
        // This can query faster at tht price of db size and breaking od db normal form.
        // Need re-visit
        [Required]
        [ForeignKey("Account")]
        public Guid BillToId { get; set; }

        [Required]
        public decimal PayAmount { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime PayDate { get; set; }

        [Required]
        public string Status { get; set; }
    }
}
