using AutoMapper;
using InvoicePaymentServices.Core.Interfaces.Repositories;
using InvoicePaymentServices.Core.Models;
using InvoicePaymentServices.Infra.DBContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoicePaymentServices.Infra.Repositories
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly InvoicePaymentDBContext _dbcontext;
        private readonly IMapper _mapper;

        public PaymentRepository(InvoicePaymentDBContext dbcontext, IMapper mapper)
        {
            _dbcontext = dbcontext ?? throw new ArgumentNullException(nameof(dbcontext));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<IEnumerable<Payment>> GetPaymentsByAccountId(Guid accountId)
        {
            var payments = await _dbcontext.Payment.Where(x => x.BillToId == accountId).ToListAsync().ConfigureAwait(false);
            if (payments != null)
            {
                return _mapper.Map<IEnumerable<Payment>>(payments);
            }

            return null;
        }

        public async Task<IEnumerable<Payment>> GetPaymentsByInvoiceId(int invoiceId)
        {
            var payments = await _dbcontext.Payment.Where(x => x.InvoiceId == invoiceId).ToListAsync().ConfigureAwait(false);
            if (payments != null)
            {
                return _mapper.Map<IEnumerable<Payment>>(payments);
            }

            return null;
        }

        public async Task<Payment> GetPaymentByPaymentId(int paymentId)
        {
            var payment = await _dbcontext.Payment.FirstOrDefaultAsync(x => x.Id == paymentId).ConfigureAwait(false);
            if (payment != null)
            {
                return _mapper.Map<Payment>(payment);
            }

            return null;
        }

        public async Task<Payment> SchedulePayment(Payment payment)
        {
            var previousPayments = await GetPaymentsByInvoiceId(payment.InvoiceId);
            string errorMessage = string.Empty;

            // Depending on how often we need to verify certain parameters,
            // We may want to extract a method or write some annotation to do this.
            if(!VerifyPayment(payment, previousPayments, out errorMessage))
            {
                throw new InvalidOperationException(errorMessage);
            }

            var dbPayment = _mapper.Map<DBEntities.Payment>(payment);
            await _dbcontext.AddAsync(dbPayment);
            await _dbcontext.SaveChangesAsync();
            payment.Id = dbPayment.Id;
            return payment;
        }

        // return true if payment passed is valid. False otherwise.
        private bool VerifyPayment(Payment payment, IEnumerable<Payment> previousPayments, out string errorMessage)
        {    
            errorMessage = string.Empty;
            if (payment == null)
            {
                errorMessage += "Empty request body.  ";
                return false;
            }

            decimal paidAmount = 0m;

            foreach (var prev in previousPayments.Where(x => x.Status.Equals("Paid", StringComparison.OrdinalIgnoreCase) || x.Status.Equals("Scheduled", StringComparison.OrdinalIgnoreCase)))
            {
                paidAmount += prev.PayAmount;
            }


            if (payment.PayAmount <= 0 || payment.PayAmount > payment.InvoiceAmount - paidAmount)
            {
                errorMessage += "The amount is not valid. Please double check.  ";                
            }

            // Not sure if we need to check if the pay date is after invoice due date
            // so skip it for now. May need re-visit.
            if(payment.PayDate < DateTime.Now.Date)
            {
                errorMessage += "The amount is not valid. Please double check.  ";
            }

            return string.IsNullOrWhiteSpace(errorMessage) ? true : false;
        }


    }
}
