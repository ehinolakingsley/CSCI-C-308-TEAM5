using CSCI_308_TEAM5.API.Security;
using Microsoft.AspNetCore.Mvc;

namespace CSCI_308_TEAM5.API.Actions.Authentication
{
    public interface IAuthenticationAction
    {
        Task<IActionResult> authenticate(Roles role, string emailAddress);

        Task<IActionResult> logOut();

        Task<IActionResult> addUser(UserArgs args);

        Task<IActionResult> addRider(RiderArgs args);

        Task<IActionResult> getProfileInfo();
    }
}
