using InvoicePaymentServices.Core.Interfaces.Services;
using InvoicePaymentServices.Core.Models;
using InvoicePaymentServices.Core.Services;
using Microsoft.AspNetCore.Mvc;
using static System.Net.Mime.MediaTypeNames;

namespace InvoicePaymentServices.Api.V1.Controllers
{
    [Produces(Application.Json)]
    [Route("api/v{api-version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    [ApiController]
    public class PaymentHistoryController : ControllerBase
    {
        private readonly IPaymentHistoryService _paymentHistoryService;
        private readonly ILogger<PaymentHistoryController> _logger;

        public PaymentHistoryController(IPaymentHistoryService paymentHistoryService, ILogger<PaymentHistoryController> logger)
        {
            _paymentHistoryService = paymentHistoryService ?? throw new ArgumentNullException(nameof(paymentHistoryService));
            _logger = logger;
        }

        [HttpGet(Name = "GetPaymentsByAccountId")]
        //[SwaggerOperation("GetFeedbacks")]
        //[Route("getfeedbacks")]
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

            var response = await _paymentHistoryService.GetPaymentsByAccountId(accountId).ConfigureAwait(false);
            return response == null ? NotFound() : Ok(response);
        }

        [HttpGet(Name = "GetPaymentsByInvoiceId")]
        //[SwaggerOperation("GetFeedbacks")]
        //[Route("getfeedbacks")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<Payment>>> GetPaymentsByInvoiceId(Guid invoiceId)
        {
            if (invoiceId == Guid.Empty)
            {
                return BadRequest("Please input a GUID.");
            }

            var response = await _paymentHistoryService.GetPaymentsByInvoiceId(invoiceId).ConfigureAwait(false);
            return response == null ? NotFound() : Ok(response);
        }

    }
}
