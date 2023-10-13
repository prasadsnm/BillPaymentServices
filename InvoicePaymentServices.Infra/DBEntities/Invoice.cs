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
        [Key]
        [Required]
        public Guid Id { get; set; }
        [Required]
        public decimal Amount { get; set; }
        [Required]
        public decimal UnpaidAmount { get; set; }
       
        [Required]
        public InvoiceStatus Status  { get; set; }
        [Required]
        [DataType(DataType.DateTime)]
        public DateTime CreatedDate { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime DueDate { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public string Description { get; set; }
    }
}
