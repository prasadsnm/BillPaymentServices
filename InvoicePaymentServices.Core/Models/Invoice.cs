using InvoicePaymentServices.Core.Enums;

namespace InvoicePaymentServices.Core.Models
{
    public class Invoice
    {
        public int Id { get; set; }
        public Guid BillToId { get; set; }
        public Guid VendorId { get; set; }
        public decimal Amount { get; set; }
        public decimal UnpaidAmount {  get; set; }
        public string Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime DueDate { get; set; }
        public string Description { get; set; }
        public string FileLink { get; set; }

        // This is to make frontend dev life easier.
        // Some available actions/links can be constructed by backend.
        public string AvailableAction { get; set; }
        

    }
}