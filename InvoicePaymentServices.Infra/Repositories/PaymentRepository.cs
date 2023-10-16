using AutoMapper;
using InvoicePaymentServices.Core.Enums;
using InvoicePaymentServices.Core.Interfaces.Repositories;
using InvoicePaymentServices.Core.Models;
using InvoicePaymentServices.Infra.DBContext;
using InvoicePaymentServices.Infra.DBEntities;
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

        public async Task<IEnumerable<Core.Models.Payment>> GetPaymentsByAccountId(Guid accountId)
        {
            var payments = await _dbcontext.Payment
                .Where(x => EF.Functions.Like(x.BillToId.ToString(), accountId.ToString()))
                .ToListAsync().ConfigureAwait(false);
            if (payments != null)
            {
                return _mapper.Map<IEnumerable<Core.Models.Payment>>(payments);
            }

            return null;
        }

        public async Task<IEnumerable<Core.Models.Payment>> GetPaymentsByInvoiceId(int invoiceId)
        {
            var payments = await _dbcontext.Payment.Where(x => x.InvoiceId == invoiceId).ToListAsync().ConfigureAwait(false);
            if (payments != null)
            {
                return _mapper.Map<IEnumerable<Core.Models.Payment>>(payments);
            }

            return null;
        }

        public async Task<Core.Models.Payment> GetPaymentByPaymentId(int paymentId)
        {
            var payment = await _dbcontext.Payment.FirstOrDefaultAsync(x => x.Id == paymentId).ConfigureAwait(false);
            if (payment != null)
            {
                return _mapper.Map<Core.Models.Payment>(payment);
            }

            return null;
        }


        public async Task<bool> UpdatePaymentAndInvoice(int paymentId, string status)
        {
            var payment = await _dbcontext.Payment.FirstOrDefaultAsync(x => x.Id == paymentId).ConfigureAwait(false);
            if (payment == null)
            {
                throw new ArgumentException($"Payment Id {paymentId} does not exist.");
            }

            payment.Status = status;
            await _dbcontext.SaveChangesAsync();

            decimal paidAmount = await GetPreviousPaymentsByInviceId(payment.InvoiceId, new string[] { "Paid" });
            var invoice = await _dbcontext.Invoice.FirstOrDefaultAsync(x => x.Id == payment.InvoiceId).ConfigureAwait(false);
            if (invoice == null)
            {
                throw new ArgumentException($"Invoice Id {payment.InvoiceId} does not exist.");
            }

            // Depending on the business logic, we may want to warn the client if the amount is bigger than invoice amount.
            if (paidAmount + payment.PayAmount >= invoice.Amount)
            {
                // Should we double check if the invoice status is already paid?
                _dbcontext.Entry(invoice).State = EntityState.Modified;
                invoice.Status = "Paid";
                _dbcontext.Update(invoice);
                await _dbcontext.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<Core.Models.Payment> SchedulePayment(Core.Models.Payment payment)
        {
            var previousPayments = await _dbcontext.Payment.Where(x => x.InvoiceId == payment.InvoiceId)
                .ToListAsync().ConfigureAwait(false);
            string errorMessage = string.Empty;

            // Depending on how often we need to verify certain parameters,
            // We may want to extract a method or write some annotation to do this.
            var errors = new List<string>();
            if (!await VerifyPayment(payment, errors))
            {
                throw new InvalidOperationException(errors.ToString());
            }

            var dbPayment = _mapper.Map<DBEntities.Payment>(payment);
            dbPayment.Status = "Scheduled";
            await _dbcontext.AddAsync(dbPayment);
            await _dbcontext.SaveChangesAsync();
            payment.Id = dbPayment.Id;
            payment.Status = dbPayment.Status;
            return payment;
        }

        private async Task<decimal> GetPreviousPaymentsByInviceId(int invoiceId, string[] statuses)
        {
            var previousPayments = await _dbcontext.Payment.Where(x => x.InvoiceId == invoiceId)
                .ToListAsync().ConfigureAwait(false);

            decimal paidAmount = 0m;

            foreach (var prev in previousPayments
                .Where(x => statuses.Contains(x.Status)))
            {
                paidAmount += prev.PayAmount;
            }

            return paidAmount;
        }

        // return true if payment passed is valid. False otherwise.
        private async Task<bool> VerifyPayment(Core.Models.Payment payment, List<string> errorMessages)
        {
            if (payment == null)
            {
                errorMessages.Add("Empty request body.  ");
                return false;
            }

            decimal paidAmount = await GetPreviousPaymentsByInviceId(payment.InvoiceId, new string[] { "Paid", "Scheduled" });

            if (payment.PayAmount <= 0 || payment.PayAmount > payment.InvoiceAmount - paidAmount)
            {
                errorMessages.Add("The amount is not valid. Please double check.  ");
            }

            // Not sure if we need to check if the pay date is after invoice due date
            // so skip it for now. May need re-visit.
            if (payment.PayDate < DateTime.Now.Date)
            {
                errorMessages.Add("The amount is not valid. Please double check.  ");
            }

            return errorMessages.Count == 0 ? true : false;
        }
    }
}
