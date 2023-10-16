using AutoMapper;
using InvoicePaymentServices.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InvoicePaymentServices.Infra.Repositories;
using InvoicePaymentServices.Core.Models;
using InvoicePaymentServices.Tests.Api.IntegrationTests;

namespace InvoicePaymentServices.Tests.Api.IntegrationTests.Repositories
{
    public class PaymentRepositoryTests : IClassFixture<DatabaseFixture>
    {
        private readonly IMapper _mapper;
        private DatabaseFixture Fixture { get; }

        public PaymentRepositoryTests(DatabaseFixture fixture)
        {
            Fixture = fixture;

            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MapperProfile>();
            });

            _mapper = configuration.CreateMapper();
        }

        [Fact]
        public async void GetPaymentByPaymentId_PaymentDoesntExist_ReturnNull()
        {
            using (var context = Fixture.CreateContext())
            {
                var repository = new PaymentRepository(context, _mapper);
                var paymentId = 56;

                var result = await repository.GetPaymentByPaymentId(paymentId);

                Assert.Null(result);
            }
        }

        [Fact]
        public async void CreatePayment_SavesCorrectData()
        {
            var paymentId = 0;

            var request = new Payment
            {
                InvoiceId = 3,
                InvoiceAmount = 200.97m,
                BillToId = new Guid(),
                PayAmount = 10m,
                PayDate = DateTime.Parse("2023-10-20T19:33:56.466Z"),
                PaymentMethod = "Email",
                Status = "Scheduled"
            };

            using (var context = Fixture.CreateContext())
            {
                var repository = new PaymentRepository(context, _mapper);

                var payment = await repository.SchedulePayment(request);
                paymentId = payment.Id;

                var newPayment = repository.GetPaymentByPaymentId(paymentId);

                Assert.NotNull(payment);
                Assert.Equal(request.InvoiceId, newPayment.Result.InvoiceId);
                Assert.Equal(request.PayAmount, newPayment.Result.PayAmount);
                Assert.Equal(request.Status, newPayment.Result.Status);
            }

        }
    }
}
