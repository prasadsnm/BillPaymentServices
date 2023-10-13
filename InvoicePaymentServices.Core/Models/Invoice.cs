using InvoicePaymentServices.Core.Enums;

namespace InvoicePaymentServices.Core
{
    public class Invoice
    {
        public Guid Id { get; set; }
        public decimal Amount { get; set; }
        public decimal UnpaidAmount {  get; set; }
        public InvoiceStatus Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime DueDate { get; set; }
        public string Description { get; set; }

    }
}