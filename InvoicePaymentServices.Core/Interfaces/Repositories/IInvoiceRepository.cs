using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoicePaymentServices.Core.Interfaces.Repositories
{
    public interface IInvoiceRepository
    {       
            Task<IEnumerable<Invoice>> GetAllInvoices();
     
    }
}
