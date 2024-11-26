using CSCI_308_TEAM5.API.Repository;
using CSCI_308_TEAM5.API.Services.Config;
using CSCI_308_TEAM5.API.Services.Email;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CSCI_308_TEAM5.API.Actions.BaseAction
{
    class AggregateServices(IRepo repo, IConfigService configService, IEmailServices emailServices) : IAggregateServices
    {
        public IRepo repo { get; init; } = repo;

        public IConfigService configService { get; init; } = configService;

        public IEmailServices emailServices { get; init; } = emailServices;

        public IActionResult Error(HttpStatusCode statusCode, string reason) => new APIResponseHandler(statusCode, reason);

        public IActionResult Ok(object data = null) => new APIResponseHandler(data);

        public async Task<IActionResult> Execute(Func<Task<IActionResult>> action)
        {
            try
            {
                return await action();
            }
            catch (Exception ex)
            {
                return Error(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

    }
}