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
    public class PaymentHistoryService : IPaymentHistoryService
    {
        private readonly IPaymentHistoryRepository _paymentHistoryRepository;
        private readonly ILogger<PaymentHistoryService> _logger;

        public PaymentHistoryService(IPaymentHistoryRepository paymentHistoryRepository, ILogger<PaymentHistoryService> logger)
        {
            _paymentHistoryRepository = paymentHistoryRepository;
            _logger = logger;
        }

        public async Task<IEnumerable<Payment>> GetPaymentsHistoryByAccountId(Guid accountId)
        {
            return null;
        }

        public async Task<IEnumerable<Payment>> GetPaymentHistsoryByInvoiceId(Guid invoiceId)
        {
         
            return null;
        }

        public Task<IEnumerable<Payment>> GetPaymentsByAccountId(Guid accountId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Payment>> GetPaymentsByInvoiceId(Guid invoiceId)
        {
            throw new NotImplementedException();
        }
    }
}
