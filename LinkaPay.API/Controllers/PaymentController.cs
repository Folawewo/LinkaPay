using LinkaPay.Application.Implementation;
using LinkaPay.Application.ServiceModels.Requests;
using LinkaPay.Application.ServiceModels.Responses;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace LinkaPay.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentController : ControllerBase
    {
        private readonly PaymentService _paymentService;

        public PaymentController(PaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpPost("create")]
        [ProducesResponseType(typeof(FlutterwavePaymentResponse), 200)]
        [ProducesResponseType(typeof(string), 400)]
        public async Task<IActionResult> CreatePayment([FromBody] CreatePaymentLinkRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid request");
            }

            try
            {
                var paymentResponse = await _paymentService.CreatePaymentLink(request);
                return Ok(paymentResponse);
            }
            catch (Exception ex)
            {
                return BadRequest($"Payment creation failed: {ex.Message}");
            }
        }
    }

}
