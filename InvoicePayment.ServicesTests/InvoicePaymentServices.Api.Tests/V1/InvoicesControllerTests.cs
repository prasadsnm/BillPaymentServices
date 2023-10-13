using AutoFixture;
using FluentAssertions;
using InvoicePaymentServices.Core;
using InvoicePaymentServices.Core.Interfaces.Services;
using InvoicePaymentServices.V1.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace InvoicePaymentServices.Tests.InvoicePaymentServices.Api.Tests.V1
{
    public class InvoicesControllerTests
    {
        private readonly IFixture _fixture;
        private readonly Mock<IInvoiceService> _serviceMoq;
        private readonly Mock<ILogger<InvoicesController>> _loggerMoq;
        private readonly InvoicesController _target;

        public InvoicesControllerTests()
        {
            _fixture = new Fixture();
            _serviceMoq = _fixture.Freeze<Mock<IInvoiceService>>();
            _loggerMoq = _fixture.Freeze<Mock<ILogger<InvoicesController>>>();
            _target = new InvoicesController(_serviceMoq.Object, _loggerMoq.Object);
        }

        [Fact]
        public async void GetInvoices_ReturnOK_WhenDataIsFound()
        {
            // Arrange
            var invoiceMoq = _fixture.Create<IEnumerable<Invoice>>();
            _serviceMoq.Setup(x=> x.GetAllInvoices()).ReturnsAsync(invoiceMoq);

            // Act
            var result = await _target.GetInvoices().ConfigureAwait(false);

            // Assert
            _serviceMoq.Verify(x => x.GetAllInvoices(), Times.Once());

            result.Should().NotBeNull();
            result.Should().BeAssignableTo<ActionResult<IEnumerable<Invoice>>>();
            result.Result.Should().BeAssignableTo<OkObjectResult>();
            result.Result.As<OkObjectResult>().Value.Should().NotBeNull()
                .And.BeOfType(invoiceMoq.GetType());           

        }
    }
}