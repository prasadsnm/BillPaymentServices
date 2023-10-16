using InvoicePaymentServices.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoicePaymentServices.Infra.DBEntities
{
    [Table("Invoice")]
    public class Invoice
    {
        // This is probably not a good idea to use autoincrement integer here.
        // We only set it up this way to simplify the code.
        // Need re-visit.
        [Key]
        [Required]
        public int Id { get; set; }

        [ForeignKey("Account")]
        [Required]
        public Guid BillToId { get; set; }

        [ForeignKey("Account")]
        [Required]
        public Guid VendorId { get; set; }

        [Required]
        public decimal Amount { get; set; }

        [Required]
        public string Status { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime CreatedDate { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime DueDate { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public string? Description { get; set; }

        [DataType(DataType.Text)]
        public string? FileLink { get; set; }
    }
}
