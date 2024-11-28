using CSCI_308_TEAM5.API.Services.Config;
using Npgsql.TypeMapping;

namespace CSCI_308_TEAM5.API.Repository.Users
{
    interface IUsersTb
    {
        Task<UsersTbModel> get(string emailAddress);

        Task<UsersTbModel> get(Guid userId);

        Task update(Guid userId, UsersTbArgs args);

        Task<Guid> add(UsersTbArgs args);
    }

    sealed class UsersTb(IConfigService configService) : IUsersTb
    {
        public Task<Guid> add(UsersTbArgs args)
        {
            throw new NotImplementedException();
        }

        public Task<UsersTbModel> get(string emailAddress)
        {
            throw new NotImplementedException();
        }

        public Task<UsersTbModel> get(Guid userId)
        {
            throw new NotImplementedException();
        }

        public Task update(Guid userId, UsersTbArgs args)
        {
            throw new NotImplementedException();
        }
    }
}
