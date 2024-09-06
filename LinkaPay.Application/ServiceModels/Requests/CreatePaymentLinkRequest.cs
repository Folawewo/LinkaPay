using System;
namespace LinkaPay.Application.ServiceModels.Requests
{
    public class CreatePaymentLinkRequest
    {
        public decimal Amount { get; set; }
        public string RedirectUrl { get; set; }
        public Customer Customer { get; set; }
    }

    public class Customer
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
    }
}

