using InvoicePaymentServices.Core.Interfaces.Repositories;
using InvoicePaymentServices.Core.Interfaces.Services;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoicePaymentServices.Core.Services
{
    public class InvoiceService : IInvoiceService
    {
        public readonly IInvoiceRepository _invoiceRepository;
        private readonly ILogger<InvoiceService> _logger;
        public InvoiceService(IInvoiceRepository invoiceRepository, ILogger<InvoiceService> logger)
        {
            _invoiceRepository = invoiceRepository ?? throw new ArgumentNullException(nameof(invoiceRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        public async Task<IEnumerable<Invoice>> GetAllInvoices()
        {
            try
            {
                return await _invoiceRepository.GetAllInvoices();
            }
            catch (Exception exception)
            {
                _logger.LogError($"Error while trying to call GetAll in service class, Error Message = {exception}.");
                throw;
            }
        }
    }
}
