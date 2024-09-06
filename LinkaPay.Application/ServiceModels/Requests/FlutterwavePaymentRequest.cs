using System;
namespace LinkaPay.Application.ServiceModels.Requests
{
    public class FlutterwavePaymentRequest
    {
        public string tx_ref { get; set; }
        public string Amount { get; set; }
        public string Currency { get; set; } = "NGN";
        public string redirect_url { get; set; }
        public FlutterWaveCustomer Customer { get; set; } 
        public string PaymentOptions { get; set; } = "card";
    }

    public class FlutterWaveCustomer 
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
    }
}
