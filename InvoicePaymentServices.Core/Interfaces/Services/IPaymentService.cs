using InvoicePaymentServices.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoicePaymentServices.Core.Interfaces.Services
{
    public interface IPaymentService
    {
        Task<IEnumerable<Payment>> GetPaymentsByAccountId(Guid accountId);
        Task<IEnumerable<Payment>> GetPaymentsByInvoiceId(int invoiceId);
        Task<Payment> GetPaymentByPaymentId(int paymentId);
        Task<Payment> SchedulePayment(Payment payment);


    }
}
