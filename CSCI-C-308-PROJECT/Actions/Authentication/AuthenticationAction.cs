using CSCI_308_TEAM5.API.Actions.BaseAction;
using CSCI_308_TEAM5.API.Security;
using Microsoft.AspNetCore.Mvc;

namespace CSCI_308_TEAM5.API.Actions.Authentication
{
    sealed class AuthenticationAction(IAggregateServices services) : BaseTeam5Action(services), IAuthenticationAction
    {
        public Task<IActionResult> addRider(RiderArgs args)
        {
            throw new NotImplementedException();
        }

        public Task<IActionResult> addUser(UserArgs args)
        {
            throw new NotImplementedException();
        }

        public Task<IActionResult> authenticate(Roles role, string emailAddress)
        {
            throw new NotImplementedException();
        }

        public Task<IActionResult> getProfileInfo()
        {
            throw new NotImplementedException();
        }

        public Task<IActionResult> logOut()
        {
            throw new NotImplementedException();
        }
    }
}
