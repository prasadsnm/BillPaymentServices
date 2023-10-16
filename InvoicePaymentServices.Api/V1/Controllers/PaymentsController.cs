using InvoicePaymentServices.Core.Interfaces.Services;
using InvoicePaymentServices.Core.Models;
using InvoicePaymentServices.Core.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using static System.Net.Mime.MediaTypeNames;

namespace InvoicePaymentServices.Api.V1.Controllers
{
    [Produces(Application.Json)]
    [Route("api/v{api-version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {
        private readonly IPaymentService _paymentService;
        private readonly ILogger<PaymentsController> _logger;

        public PaymentsController(IPaymentService paymentHistoryService, ILogger<PaymentsController> logger)
        {
            _paymentService = paymentHistoryService ?? throw new ArgumentNullException(nameof(paymentHistoryService));
            _logger = logger;
        }

        [HttpGet("account/{accountId}", Name = "GetPaymentsByAccountId")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<Payment>>> GetPaymentsByAccountId(Guid accountId)
        {
            if (accountId == Guid.Empty)
            {
                return BadRequest("Please input a GUID.");
            }

            var response = await _paymentService.GetPaymentsByAccountId(accountId).ConfigureAwait(false);
            return response == null ? NotFound() : Ok(response);
        }

        [HttpGet("invoice/{invoiceId}", Name = "GetPaymentsByInvoiceId")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<Payment>>> GetPaymentsByInvoiceId(int invoiceId)
        {
            if (invoiceId <= 0)
            {
                return BadRequest("Please input a valid invoice id.");
            }

            var response = await _paymentService.GetPaymentsByInvoiceId(invoiceId).ConfigureAwait(false);
            return response == null ? NotFound() : Ok(response);
        }

        [HttpGet("{paymentId}", Name = "GetPaymentByPaymentId")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Payment>> GetPaymentByPaymentId(int paymentId)
        {
            var response = await _paymentService.GetPaymentByPaymentId(paymentId).ConfigureAwait(false);
            return response == null ? NotFound() : Ok(response);
        }

        [HttpPost(Name = "SchedulePayment")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Payment>> SchedulePayment(Payment payment)
        {
            if (!ModelState.IsValid )
            {
                return BadRequest();
            }

            var response = await _paymentService.SchedulePayment(payment).ConfigureAwait(false);
            return CreatedAtRoute(nameof(GetPaymentByPaymentId), new { paymentId = response.Id }, response);
        }       
    }
}
