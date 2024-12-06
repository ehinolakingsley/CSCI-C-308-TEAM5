using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json;

namespace CSCI_308_TEAM5.API.Actions.BaseAction
{
    sealed class APIResponseHandler : IActionResult
    {
        const string textType = "text/plain";
        const string jsonType = "application/json";

        readonly string contentType = jsonType;
        readonly string data;
        readonly HttpStatusCode StatusCode;

        public APIResponseHandler(HttpStatusCode statusCode, string reason)
        {
            StatusCode = statusCode;
            data = reason;

            contentType = data.stringJson() ? jsonType : textType;
        }

        public APIResponseHandler(object data)
        {
            this.data = data.ToString().stringJson() ? JsonSerializer.Serialize(data) : data.ToString();
            StatusCode = HttpStatusCode.OK;

            contentType = this.data.stringJson() ? jsonType : textType;
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
