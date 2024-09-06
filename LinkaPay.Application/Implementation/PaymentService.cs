using System;
using System.Threading.Tasks;
using LinkaPay.Application.ServiceModels.Requests;
using LinkaPay.Application.ServiceModels.Responses;

namespace LinkaPay.Application.Implementation
{
    public class PaymentService
    {
        private readonly FlutterwaveService _flutterwaveService;

        public PaymentService(FlutterwaveService flutterwaveService)
        {
            _flutterwaveService = flutterwaveService;
        }

        public async Task<FlutterwavePaymentResponse> CreatePaymentLink(CreatePaymentLinkRequest request)
        {
            var flutterwaveRequest = new FlutterwavePaymentRequest
            {
                tx_ref = Guid.NewGuid().ToString(), 
                Amount = request.Amount.ToString(),
                Currency = "NGN",
                redirect_url = request.RedirectUrl ?? "https://linkapay.com",
                Customer = new FlutterWaveCustomer
                {
                    Email = request.Customer.Email,
                    Name = request.Customer.Name,
                    PhoneNumber = request.Customer.PhoneNumber
                },
                PaymentOptions = "card"
            };

            var flutterwaveResponse = await _flutterwaveService.InitiatePayment(flutterwaveRequest);

            return flutterwaveResponse;
        }

    }
}
