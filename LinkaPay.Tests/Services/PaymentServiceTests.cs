using System;
using System.Threading.Tasks;
using Xunit;
using Moq;
using LinkaPay.Application.Implementation;
using LinkaPay.Application.ServiceModels.Requests;
using LinkaPay.Application.ServiceModels.Responses;

namespace LinkaPay.Tests.Services
{
    public class PaymentServiceTests
    {
        private readonly PaymentService _paymentService;
        private readonly Mock<FlutterwaveService> _flutterwaveServiceMock;

        public PaymentServiceTests()
        {
            _flutterwaveServiceMock = new Mock<FlutterwaveService>(null, null);
            _paymentService = new PaymentService(_flutterwaveServiceMock.Object);
        }

        [Fact]
        public async Task CreatePaymentLink_ShouldReturnSuccess_WhenFlutterwaveInitiatesPaymentSuccessfully()
        {
            var paymentLinkRequest = new CreatePaymentLinkRequest
            {
                Amount = 5000,
                RedirectUrl = "https://linkapay.co",
                Customer = new Customer
                {
                    Email = "anthony@anthony.com",
                    Name = "Anthony Anthony",
                    PhoneNumber = "123456789"
                }
            };

            var flutterwaveResponse = new FlutterwavePaymentResponse
            {
                Status = "success",
                Message = "Payment link created successfully",
                data = new FlutterwavePaymentResponseData
                {
                    Link = "https://pay.flutterwave.com/test"
                }
            };

            _flutterwaveServiceMock
                .Setup(f => f.InitiatePayment(It.IsAny<FlutterwavePaymentRequest>()))
                .ReturnsAsync(flutterwaveResponse);

            var result = await _paymentService.CreatePaymentLink(paymentLinkRequest);

            Assert.NotNull(result);
            Assert.Equal("success", result.Status);
            Assert.Equal("https://pay.flutterwave.com/test", result.data.Link); 
        }


        [Fact]
        public async Task CreatePaymentLink_ShouldThrowException_WhenFlutterwaveReturnsError()
        {
            var paymentLinkRequest = new CreatePaymentLinkRequest
            {
                Amount = 5000,
                RedirectUrl = "https://linkapay.co",
                Customer = new Customer
                {
                    Email = "anthony@anthony.com",
                    Name = "Anthony Anthony",
                    PhoneNumber = "123456789"
                }
            };

            _flutterwaveServiceMock
                .Setup(f => f.InitiatePayment(It.IsAny<FlutterwavePaymentRequest>()))
                .ThrowsAsync(new Exception("Error initiating payment"));

            await Assert.ThrowsAsync<Exception>(() => _paymentService.CreatePaymentLink(paymentLinkRequest));
        }
    }
}
