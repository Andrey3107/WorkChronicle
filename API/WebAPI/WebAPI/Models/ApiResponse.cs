namespace WebAPI.Models
{
    using System.Net;

    using Dto;

    public class ApiResponse<T>
    {
        public HttpStatusCode StatusCode { get; set; }

        public bool IsSuccess { get; set; }

        public string ErrorMessage { get; set; }

        public T Result { get; set; }
    }
}
