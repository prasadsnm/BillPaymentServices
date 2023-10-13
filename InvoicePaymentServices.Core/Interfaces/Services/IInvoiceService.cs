using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoicePaymentServices.Core.Interfaces.Services
{
    public interface IInvoiceService
    {
        Task<IEnumerable<Invoice>> GetAllInvoices();
        // Task<Invoice> GetInvoiceById(int id);
    }
}
