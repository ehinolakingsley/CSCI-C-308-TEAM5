using CSCI_308_TEAM5.API.Repository;
using CSCI_308_TEAM5.API.Security;
using CSCI_308_TEAM5.API.Services.Config;
using CSCI_308_TEAM5.API.Services.Email;

namespace CSCI_308_TEAM5.API.Actions.BaseAction
{
    class AggregateServices(IRepo repo, IConfigService configService, IEmailServices emailServices, IUserIdentity userIdentity) : IAggregateServices
    {
        public IRepo repo { get; init; } = repo;

        public IConfigService configService { get; init; } = configService;

        public IEmailServices emailServices { get; init; } = emailServices;

        public IUserIdentity userIdentity { get; init; } = userIdentity;

        public static IActionResult Error(HttpStatusCode statusCode, string reason) => new APIResponseHandler(statusCode, reason);

        public static IActionResult Ok(object data = null) => new APIResponseHandler(data);

        public static async Task<IActionResult> Execute(Func<Task<IActionResult>> action)
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