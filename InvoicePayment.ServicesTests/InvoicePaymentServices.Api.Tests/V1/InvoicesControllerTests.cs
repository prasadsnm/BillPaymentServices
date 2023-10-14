using AutoFixture;
using FluentAssertions;
using InvoicePaymentServices.Core;
using InvoicePaymentServices.Core.Interfaces.Services;
using InvoicePaymentServices.Core.Models;
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
        private readonly Guid _guidMoq = Guid.NewGuid();
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
        public async void GetInvoicesByAccountId_ReturnOK_WhenDataIsFound()
        {
            // Arrange
            var invoiceMoq = _fixture.Create<IEnumerable<Invoice>>();
            _serviceMoq.Setup(x => x.GetInvoicesByAccountId(_guidMoq)).ReturnsAsync(invoiceMoq);

            // Act
            var result = await _target.GetInvoicesByAccountId(_guidMoq).ConfigureAwait(false);

            // Assert
            _serviceMoq.Verify(x => x.GetInvoicesByAccountId(_guidMoq), Times.Once());

            result.Should().NotBeNull();
            result.Should().BeAssignableTo<ActionResult<IEnumerable<Invoice>>>();
            result.Result.Should().BeAssignableTo<OkObjectResult>();
            result.Result.As<OkObjectResult>().Value.Should().NotBeNull()
                .And.BeOfType(invoiceMoq.GetType());
        }

        [Fact]
        public async void GetInvoicesByAccountId_ReturnsNotFound_WhenDataIsNotFound()
        {
            // Arrange
            List<Invoice> nullInvoice = null;
            _serviceMoq.Setup(x => x.GetInvoicesByAccountId(_guidMoq)).ReturnsAsync(nullInvoice);

            // Act
            var result = await _target.GetInvoicesByAccountId(_guidMoq).ConfigureAwait(false);

            // Assert
            _serviceMoq.Verify(x => x.GetInvoicesByAccountId(_guidMoq), Times.Once());

            result.Should().NotBeNull();
            result.Result.Should().BeAssignableTo<NotFoundResult>();
        }

        [Fact]
        public async void GetInvoicesByAccountId_ReturnsBadRequest_WhenGetEmptyGuid()
        {
            // Arrange
            var invoiceMoq = _fixture.Create<IEnumerable<Invoice>>();
            _serviceMoq.Setup(x => x.GetInvoicesByAccountId(_guidMoq)).ReturnsAsync(invoiceMoq);

            // Act
            var result = await _target.GetInvoicesByAccountId(Guid.Empty).ConfigureAwait(false);

            // Assert
            _serviceMoq.Verify(x => x.GetInvoicesByAccountId(It.IsAny<Guid>()), Times.Never());

            result.Should().NotBeNull();
            result.Result.Should().BeAssignableTo<BadRequestObjectResult>();
        }

        [Fact]
        public void CreateController_ThrowsException_WhenParameterIsNull()
        {
            IInvoiceService nullInterface = null;
            Assert.Throws<ArgumentNullException>(()=> new InvoicesController(nullInterface, _loggerMoq.Object));
        }
    }
}