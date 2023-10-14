using InvoicePaymentServices.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoicePaymentServices.Core.Interfaces.Repositories
{
    public interface IPaymentHistoryRepository
    {
        Task<IEnumerable<Payment>> GetPaymentByAccountId(Guid accountId);
        Task<IEnumerable<Payment>> GetPaymentByInvoiceId(Guid invoiceId);
    }
}
