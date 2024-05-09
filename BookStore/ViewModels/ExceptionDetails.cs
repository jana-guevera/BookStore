using System.Collections.Generic;

namespace BookStore.ViewModels
{
    public class ExceptionDetails
    {
        public string Message { get; set; }
        public int StatusCode { get; set; }
        public bool IsApplicationError { get; set; }
        public string ExceptionType { get; set; }
        public IDictionary<string, string[]> Errors { get; set; } = new Dictionary<string, string[]>();
    }
}
