using AutoFixture;
using FluentAssertions;
using InvoicePaymentServices.Api.V1.Controllers;
using InvoicePaymentServices.Core.Interfaces.Services;
using InvoicePaymentServices.Core.Models;
using InvoicePaymentServices.V1.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoicePaymentServices.Tests.InvoicePaymentServices.Api.Tests.V1
{
    public class PaymentHistoryControllerTests
    {
        private readonly IFixture _fixture;
        private readonly Mock<IPaymentService> _serviceMoq;
        private readonly Guid _guidMoq = Guid.NewGuid();
        private readonly Mock<ILogger<PaymentsController>> _loggerMoq;
        private readonly PaymentsController _target;

        public PaymentHistoryControllerTests()
        {
            _fixture = new Fixture();
            _serviceMoq = _fixture.Freeze<Mock<IPaymentService>>();
            _loggerMoq = _fixture.Freeze<Mock<ILogger<PaymentsController>>>();
            _target = new PaymentsController(_serviceMoq.Object, _loggerMoq.Object);
        }

        [Fact]
        public async Task GetPaymentHistoryByAccountId_ReturnsOk_WhenDataIsFound()
        {
            // Arrange
            var paymentsHistoryMoq = _fixture.Create<IEnumerable<Payment>>();
            _serviceMoq.Setup(x => x.GetPaymentsByAccountId(_guidMoq)).ReturnsAsync(paymentsHistoryMoq);

            // Act
            var result = await _target.GetPaymentsByAccountId(_guidMoq).ConfigureAwait(false);

            // Assert
            _serviceMoq.Verify(x => x.GetPaymentsByAccountId(_guidMoq), Times.Once());

            result.Should().NotBeNull();
            result.Should().BeAssignableTo<ActionResult<IEnumerable<Payment>>>();
            result.Result.As<OkObjectResult>().Value.Should().NotBeNull()
                .And.BeOfType(paymentsHistoryMoq.GetType());
        }

        [Fact]
        public async Task GetPaymentHistoryByAccountId_ReturnsNotFound_WhenDataIsNotFound()
        {
            // Arrange
            List<Payment> nullPaymentHistory = null;
            _serviceMoq.Setup(x => x.GetPaymentsByAccountId(_guidMoq)).ReturnsAsync(nullPaymentHistory);

            // Act
            var result = await _target.GetPaymentsByAccountId(_guidMoq).ConfigureAwait(false);

            // Assert
            _serviceMoq.Verify(x => x.GetPaymentsByAccountId(_guidMoq), Times.Once);
            result.Should().NotBeNull();
            result.Result.Should().BeAssignableTo<NotFoundResult>();
        }

        [Fact]
        public async Task GetPaymentHistoryByAccountId_ReturnsBadRequest_WhenGetEmptyGuid()
        {
            // Arrange
            var paymentsHistoryMoq = _fixture.Create<IEnumerable<Payment>>();
            _serviceMoq.Setup(x => x.GetPaymentsByAccountId(It.IsAny<Guid>())).ReturnsAsync(paymentsHistoryMoq);

            // Act
            var result = await _target.GetPaymentsByAccountId(Guid.Empty).ConfigureAwait(false);

            // Assert
            _serviceMoq.Verify(x => x.GetPaymentsByAccountId(It.IsAny<Guid>()), Times.Never());

            result.Should().NotBeNull();
            result.Result.Should().BeAssignableTo<BadRequestObjectResult>();
        }

        [Fact]
        public void GetPaymentHistoryByAccountId_ThrowsException_WhenParameterIsNull()
        {
            IPaymentService _service = null;
            Assert.Throws<ArgumentNullException>(() => new PaymentsController(_service, _loggerMoq.Object));
        }

        [Fact]
        public async Task GetPaymentsHistoryByInvoiceId_ReturnsOk_WhenDataIsFound()
        {
            // Arrange
            var paymentsHistoryMoq = _fixture.Create<IEnumerable<Payment>>();
            var id = _fixture.Create<int>();
            _serviceMoq.Setup(x => x.GetPaymentsByInvoiceId(id)).ReturnsAsync(paymentsHistoryMoq);

            // Act
            var result = await _target.GetPaymentsByInvoiceId(id).ConfigureAwait(false);

            // Assert
            _serviceMoq.Verify(x => x.GetPaymentsByInvoiceId(id), Times.Once());

            result.Should().NotBeNull();
            result.Should().BeAssignableTo<ActionResult<IEnumerable<Payment>>>();
            result.Result.As<OkObjectResult>().Value.Should().NotBeNull()
                .And.BeOfType(paymentsHistoryMoq.GetType());
        }

        [Fact]
        public async Task GetPaymentsHistoryByInvoiceId_ReturnsNotFound_WhenDataIsNotFound()
        {
            // Arrange
            List<Payment> nullPaymentHistory = null;
            var id = _fixture.Create<int>();
            _serviceMoq.Setup(x => x.GetPaymentsByInvoiceId(id)).ReturnsAsync(nullPaymentHistory);

            // Act
            var result = await _target.GetPaymentsByInvoiceId(id).ConfigureAwait(false);

            // Assert
            _serviceMoq.Verify(x => x.GetPaymentsByInvoiceId(id), Times.Once);
            result.Should().NotBeNull();
            result.Result.Should().BeAssignableTo<NotFoundResult>();
        }

        [Fact]
        public async Task GetPaymentsHistoryByInvoiceId_ReturnsBadRequest_WhenGetZeroId()
        {
            // Arrange
            var paymentsHistoryMoq = _fixture.Create<IEnumerable<Payment>>();
            var id = 0;
            _serviceMoq.Setup(x => x.GetPaymentsByInvoiceId(id)).ReturnsAsync(paymentsHistoryMoq);

            // Act
            var result = await _target.GetPaymentsByInvoiceId(id).ConfigureAwait(false);

            // Assert
            _serviceMoq.Verify(x => x.GetPaymentsByInvoiceId(id), Times.Never());

            result.Should().NotBeNull();
            result.Result.Should().BeAssignableTo<BadRequestObjectResult>();
        }

        [Fact]
        public async Task GetPaymentsHistoryByInvoiceId_ReturnsBadRequest_WhenGetNegativeId()
        {
            // Arrange
            var paymentsHistoryMoq = _fixture.Create<IEnumerable<Payment>>();
            var id = -1;
            _serviceMoq.Setup(x => x.GetPaymentsByInvoiceId(id)).ReturnsAsync(paymentsHistoryMoq);

            // Act
            var result = await _target.GetPaymentsByInvoiceId(id).ConfigureAwait(false);

            // Assert
            _serviceMoq.Verify(x => x.GetPaymentsByInvoiceId(id), Times.Never());

            result.Should().NotBeNull();
            result.Result.Should().BeAssignableTo<BadRequestObjectResult>();
        }
    }
}
