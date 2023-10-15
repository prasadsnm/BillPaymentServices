using InvoicePaymentServices.Core;
using InvoicePaymentServices.Core.Interfaces.Services;
using InvoicePaymentServices.Core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using static System.Net.Mime.MediaTypeNames;
namespace InvoicePaymentServices.V1.Controllers
{
    [Produces(Application.Json)]
    [Route("api/v{api-version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    [ApiController]
    public class InvoicesController : ControllerBase
    {
        private readonly IInvoiceService _invoiceService;
        private readonly ILogger<InvoicesController> _logger;

        public InvoicesController(IInvoiceService invoiceService, ILogger<InvoicesController> logger)
        {
            this._invoiceService = invoiceService ?? throw new ArgumentNullException(nameof(invoiceService));
            this._logger = logger;
        }

        [HttpGet("{accountId}", Name = "GetInvoiceByAccountId")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<Invoice>>> GetInvoicesByAccountId(Guid accountId)
        {
            // This is probably not needed. 
            if (accountId == Guid.Empty)
            {
                return BadRequest("Please input a GUID");
            }

            var response = await _invoiceService.GetInvoicesByAccountId(accountId).ConfigureAwait(false);
            return response == null ? NotFound() : Ok(response);
        }
    }
}