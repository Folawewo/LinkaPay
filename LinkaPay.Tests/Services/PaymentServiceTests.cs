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
            // Arrange
            var paymentLinkRequest = new CreatePaymentLinkRequest
            {
                Amount = 5000,
                RedirectUrl = "https://linkapay.co",
                Customer = new Customer
                {
                    Email = "test@example.com",
                    Name = "John Doe",
                    PhoneNumber = "08012345678"
                }
            };

            var flutterwaveResponse = new FlutterwavePaymentResponse
            {
                Status = "success",
                Message = "Payment link created successfully",
                RedirectUrl = "https://pay.flutterwave.com/test"
            };

            // Mock the response from the FlutterwaveService
            _flutterwaveServiceMock
                .Setup(f => f.InitiatePayment(It.IsAny<FlutterwavePaymentRequest>()))
                .ReturnsAsync(flutterwaveResponse);

            // Act
            var result = await _paymentService.CreatePaymentLink(paymentLinkRequest);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("success", result.Status);
            Assert.Equal("https://pay.flutterwave.com/test", result.RedirectUrl);
        }

        [Fact]
        public async Task CreatePaymentLink_ShouldThrowException_WhenFlutterwaveReturnsError()
        {
            // Arrange
            var paymentLinkRequest = new CreatePaymentLinkRequest
            {
                Amount = 5000,
                RedirectUrl = "https://linkapay.co",
                Customer = new Customer
                {
                    Email = "test@example.com",
                    Name = "John Doe",
                    PhoneNumber = "08012345678"
                }
            };

            // Mock an error response from Flutterwave
            _flutterwaveServiceMock
                .Setup(f => f.InitiatePayment(It.IsAny<FlutterwavePaymentRequest>()))
                .ThrowsAsync(new Exception("Error initiating payment"));

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => _paymentService.CreatePaymentLink(paymentLinkRequest));
        }
    }
}
