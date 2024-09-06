using System;
namespace LinkaPay.Application.ServiceModels.Responses
{
    public class BaseResponse
    {

        public string status { get; set; }
        public string message { get; set; }

        public string ResponseCode { get; set; }

        public string ResponseDescription { get; set; }
    }
}

