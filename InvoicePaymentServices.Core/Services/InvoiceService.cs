using InvoicePaymentServices.Core.Interfaces.Repositories;
using InvoicePaymentServices.Core.Interfaces.Services;
using InvoicePaymentServices.Core.Models;
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
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly ILogger<InvoiceService> _logger;

        public InvoiceService(IInvoiceRepository invoiceRepository, ILogger<InvoiceService> logger)
        {
            _invoiceRepository =
                invoiceRepository ?? throw new ArgumentNullException(nameof(invoiceRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<IEnumerable<Invoice>> GetInvoicesByAccountId(Guid accountId)
        {
            try
            {
                return await _invoiceRepository.GetInvoicesByAccountId(accountId);
            }
            catch (Exception exception)
            {
                _logger.LogError(
                    $"Error while trying to call GetAllInvoices in service class, Error Message = {exception}."
                );
                throw;
            }
        }
    }
}
