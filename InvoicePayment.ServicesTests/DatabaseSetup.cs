using Bogus;
using Bogus.DataSets;
using InvoicePaymentServices.Core.Enums;
using InvoicePaymentServices.Infra.DBContext;
using InvoicePaymentServices.Infra.DBEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoicePaymentServices.Tests
{
    public static class DatabaseSetup
    {
        public static void SeedData(InvoicePaymentDBContext context)
        {
            // For payment db
            context.Payment.RemoveRange(context.Payment);

            var payment = 1;
            var fakePayments = new Faker<Payment>()
                .RuleFor(o => o.Id, f => payment++)
                .RuleFor(o => o.InvoiceId, f => f.Random.Int())
                .RuleFor(o => o.BillToId, f => f.Random.Guid())
                .RuleFor(o => o.PayAmount, f => f.Random.Decimal(1, 50))
                .RuleFor(o => o.Status, f => f.PickRandom<PaymentStatus>().ToString())
                .RuleFor(o => o.PayDate, f => f.Date.Future(1, DateTime.Now));

            var payments = fakePayments.Generate(10);

            context.AddRange(payments);


            // For invoice db
            context.Invoice.RemoveRange(context.Invoice);

            var invoiceId = 1;            

            var fakeInvoices = new Faker<Invoice>()
                .RuleFor(o => o.Id, f => invoiceId++)
                .RuleFor(o => o.VendorId, f => f.Random.Guid())
                .RuleFor(o => o.BillToId, f => f.Random.Guid())
                .RuleFor(o => o.Amount, f => f.Random.Decimal(100, 5000))
                .RuleFor(o => o.Status, f => f.PickRandom<PaymentStatus>().ToString())
                .RuleFor(o => o.CreatedDate, f => f.Date.Past(1, DateTime.Now))
                .RuleFor(o => o.DueDate, f => f.Date.Future(1, DateTime.Now))
                .RuleFor(o => o.FileLink, f => $"File Link {invoiceId}")
                .RuleFor(o => o.Description, f => $"Description {invoiceId}");

            // Not sure if this is right way to do the test.
            // Need re-visit
            var constantGuid = "8d8ce279-b746-4e73-a1f8-40696fc4e632";
            var invoices = new List<Invoice>();

            for (int i = 0; i < 10; i++)
            {
                var invoice = fakeInvoices.Generate();

                // Set one object's Guid to the constantGuid
                if (i == 0)
                {
                    invoice.BillToId = Guid.Parse(constantGuid);
                }

                invoices.Add(invoice);
            }

            context.AddRange(invoices);


            context.SaveChanges();
        }
    }
}
