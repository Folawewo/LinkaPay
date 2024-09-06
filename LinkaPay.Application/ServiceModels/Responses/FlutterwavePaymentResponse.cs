using System;
namespace LinkaPay.Application.ServiceModels.Responses
{
    public class FlutterwavePaymentResponse
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public FlutterwavePaymentResponseData data { get; set; }
    }

    public class FlutterwavePaymentResponseData
    {
        public string Link { get; set; }
        public string Message { get; set; }
    }

}

