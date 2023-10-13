using InvoicePaymentServices.Core;
using InvoicePaymentServices.Core.Interfaces.Services;
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
            this._invoiceService= invoiceService?? throw new ArgumentNullException(nameof(invoiceService));
            this._logger = logger;
        }

        [HttpGet(Name = "GetInvoice")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<Invoice>>> GetInvoices()
        {
            var response = await _invoiceService.GetAllInvoices().ConfigureAwait(false);
            return response != null ? Ok(response) : NotFound();

            
        }
    }
}