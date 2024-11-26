using CSCI_308_TEAM5.API.Repository.Authentication;
using CSCI_308_TEAM5.API.Repository.Role;
using CSCI_308_TEAM5.API.Repository.Users;

namespace CSCI_308_TEAM5.API.Repository
{
    sealed class Repo(IAuthenticationTb authenticationTb, IRoleTb roleTb, IUsersTb usersTb) : IRepo
    {
        public IAuthenticationTb authenticationTb { get; } = authenticationTb;

        public IRoleTb roleTb { get; } = roleTb;

        public IUsersTb usersTb { get; } = usersTb;
    }
}
