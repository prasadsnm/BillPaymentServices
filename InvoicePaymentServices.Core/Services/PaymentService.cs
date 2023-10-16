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
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly ILogger<PaymentService> _logger;

        public PaymentService(
            IPaymentRepository paymentRepository,
            ILogger<PaymentService> logger
        )
        {
            _paymentRepository = paymentRepository;
            _logger = logger;
        }


        public async Task<IEnumerable<Payment>> GetPaymentsByAccountId(Guid accountId)
        {
            try
            {
                return await _paymentRepository.GetPaymentsByAccountId(accountId);
            }
            catch (Exception exception)
            {
                _logger.LogError(
                    $"Error while trying to call GetPaymentsByAccountId in service class, Error Message = {exception}."
                );
                throw;
            }
        }

        public async Task<IEnumerable<Payment>> GetPaymentsByInvoiceId(int invoiceId)
        {
            try
            {
                return await _paymentRepository.GetPaymentsByInvoiceId(invoiceId);
            }
            catch (Exception exception)
            {
                _logger.LogError(
                    $"Error while trying to call GetPaymentsByInvoiceId in service class, Error Message = {exception}."
                );
                throw;
            }
        }

        public async Task<Payment> GetPaymentByPaymentId(int paymentId)
        {
            try
            {
                return await _paymentRepository.GetPaymentByPaymentId(paymentId);
            }
            catch (Exception exception)
            {
                _logger.LogError(
                    $"Error while trying to call GetPaymentByPaymentId in service class, Error Message = {exception}."
                );
                throw;
            }
        }

        public async Task<Payment> SchedulePayment(Payment payment)
        {
            try
            {
                return await _paymentRepository.SchedulePayment(payment);
            }
            catch (Exception exception)
            {
                _logger.LogError(
                    $"Error while trying to call SchedulePayment in service class, Error Message = {exception}."
                );
                throw;
            }
        }

        public async Task<bool> UpdatePaymentAndInvoice(int paymentId, string status)
        {
            try
            {
                if (paymentId <= 0)
                {
                    throw new ArgumentNullException(nameof(paymentId));
                }

                return await _paymentRepository.UpdatePaymentAndInvoice(paymentId, status);
            }
            catch (Exception exception)
            {
                _logger.LogError($"Error while trying to call UpdatePaymentAndInvoice in service class, Error Message = {exception}.");
                throw;
            }
        }


    }
}
