namespace WepApi.Controllers
{
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;
    using System.Web.Http.ModelBinding;

    public class ApiControllerBase : ApiController
    {
        //private readonly IUnitOfWork unitOfWork;

        public ApiControllerBase()//IUnitOfWork unitOfWork)
        {
            //this.unitOfWork = unitOfWork;
        }

        //public IUnitOfWork UnitOfWork => unitOfWork;

        protected HttpResponseMessage CreateErrorResponse(Exception exception, HttpStatusCode statusCode = HttpStatusCode.InternalServerError)
        {
            return Request.CreateErrorResponse(statusCode, exception.Message);
        }

        protected HttpResponseMessage CreateErrorResponse(string message, HttpStatusCode statusCode = HttpStatusCode.InternalServerError)
        {
            return Request.CreateErrorResponse(statusCode, message);
        }

        protected HttpResponseMessage CreateErrorResponse(ModelStateDictionary state, HttpStatusCode statusCode = HttpStatusCode.InternalServerError)
        {
            return Request.CreateErrorResponse(statusCode, state);
        }

        protected HttpResponseMessage CreateOkResponse<T>(T obj)
        {
            return Request.CreateResponse(HttpStatusCode.OK, obj);
        }

        protected HttpResponseMessage CreateOkResponse()
        {
            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}