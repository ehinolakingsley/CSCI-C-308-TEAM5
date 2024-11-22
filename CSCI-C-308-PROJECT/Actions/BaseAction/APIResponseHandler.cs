using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json;

namespace CSCI_308_TEAM5.API.Actions.BaseAction
{
    sealed class APIResponseHandler : IActionResult
    {
        const string contentType = "application/json";
        readonly string data;
        readonly HttpStatusCode StatusCode;

        public APIResponseHandler(HttpStatusCode statusCode, string reason)
        {
            StatusCode = statusCode;
            data = reason;
        }

        public APIResponseHandler(object data)
        {
            this.data = JsonSerializer.Serialize(data);
            StatusCode = HttpStatusCode.OK;
        }


        public Task ExecuteResultAsync(ActionContext context)
        {
            if (context is null)
                throw new ArgumentNullException(nameof(context));

            var httpContext = context.HttpContext;

            httpContext.Response.HttpContext.Features.Get<IHttpResponseFeature>().ReasonPhrase = StatusCode.Humanize().Titleize();
            httpContext.Response.StatusCode = (int)StatusCode;
            httpContext.Response.ContentType = contentType;

            switch (StatusCode)
            {
                case HttpStatusCode.NoContent:
                case HttpStatusCode.NotModified:
                    return Task.CompletedTask;
            }
            return httpContext.Response.WriteAsync(data);
        }
    }
}
