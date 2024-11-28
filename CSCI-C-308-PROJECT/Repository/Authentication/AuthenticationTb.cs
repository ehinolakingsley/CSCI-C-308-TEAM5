using CSCI_308_TEAM5.API.Security;
using CSCI_308_TEAM5.API.Services.Config;

namespace CSCI_308_TEAM5.API.Repository.Authentication
{
    interface IAuthenticationTb
    {
        Task<AuthenticationTbModel> get(Guid userId, int roleId);

        Task del(Guid userId, Roles roleId);

        Task addOrUpdate(AuthenticationTbArgs args);
    }

    sealed class AuthenticationTb(IConfigService configService) : IAuthenticationTb
    {
        public Task addOrUpdate(AuthenticationTbArgs args)
        {
            throw new NotImplementedException();
        }

        public Task del(Guid userId, Roles roleId)
        {
            throw new NotImplementedException();
        }

        public Task<AuthenticationTbModel> get(Guid userId, int roleId)
        {
            throw new NotImplementedException();
        }
    }
}
