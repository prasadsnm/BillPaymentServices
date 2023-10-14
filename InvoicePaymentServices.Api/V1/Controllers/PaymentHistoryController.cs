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

    }

    //[HttpGet]
    ////[SwaggerOperation("GetFeedbacks")]
    ////[Route("getfeedbacks")]
    //[ProducesResponseType(StatusCodes.Status200OK)]
    //[ProducesResponseType(StatusCodes.Status400BadRequest)]
    //[ProducesResponseType(StatusCodes.Status401Unauthorized)]
    //[ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    //[ProducesResponseType(StatusCodes.Status500InternalServerError)]
}
