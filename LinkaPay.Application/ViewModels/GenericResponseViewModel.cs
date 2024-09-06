using System;
namespace LinkaPay.Application.ViewModels
{
    public class GenericResponseViewModel
    {
        public string code { get; set; }
        public string message { get; set; }
        public bool IsSuccessful { get; set; }
    }

    public class GenericResponseViewModel<T> : GenericResponseViewModel
    {
        public T data { get; set; }
    }
}

