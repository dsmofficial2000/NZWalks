using System.Net;

namespace NZWalks.API.Middleware
{
    public class ExceptionHandling
    {
        private readonly ILogger<ExceptionHandling> logger;
        private readonly RequestDelegate next;

        public ExceptionHandling(ILogger<ExceptionHandling> logger, RequestDelegate next)
        {
            this.logger = logger;
            this.next = next;
        }

        public async Task InvokeAsync (HttpContext Httpcontext)
        {
            try
            {
                await next(Httpcontext);
            }
            catch (Exception ex)
            {
                var errorId = Guid.NewGuid();

                //log this exception
                logger.LogError(ex, $"{errorId} : { ex.Message}");

                //return a custom error messge 
                Httpcontext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                Httpcontext.Response.ContentType = "application/json";

                var error = new
                {
                    id = errorId,
                    ErrorMessage = ("Something went wrong! we are looking to resolve this")
                };

                await Httpcontext.Response.WriteAsJsonAsync(error);
            }
        }
    }
}
