using CSCI_308_TEAM5.API.Security;
using CSCI_308_TEAM5.API.Services.Config;

namespace CSCI_308_TEAM5.API.Repository.Authentication
{
    interface IAuthenticationTb
    {
        Task<AuthenticationTbModel> get(Guid userId, int roleId);

        Task del(Guid userId, Roles roleId);
    }

    sealed class AuthenticationTb(IConfigService configService) : IAuthenticationTb
    {

    }
}
