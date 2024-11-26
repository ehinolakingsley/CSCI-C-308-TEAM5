using CSCI_308_TEAM5.API.Repository.Authentication;
using CSCI_308_TEAM5.API.Repository.Role;
using CSCI_308_TEAM5.API.Repository.Users;

namespace CSCI_308_TEAM5.API.Repository
{
    interface IRepo
    {
        IAuthenticationTb authenticationTb { get; }

        IRoleTb roleTb { get; }

        IUsersTb usersTb { get; }
    }
}
