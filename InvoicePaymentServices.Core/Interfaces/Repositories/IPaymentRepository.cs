using InvoicePaymentServices.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoicePaymentServices.Core.Interfaces.Repositories
{
    public interface IPaymentRepository
    {
        Task<IEnumerable<Payment>> GetPaymentsByAccountId(Guid accountId);
        Task<IEnumerable<Payment>> GetPaymentsByInvoiceId(int invoiceId);
        Task<Payment> GetPaymentByPaymentId(int paymentId);

        Task<Payment> SchedulePayment(Payment payment);
        Task<bool> UpdatePaymentAndInvoice(int paymentId, string status);


    }
}
