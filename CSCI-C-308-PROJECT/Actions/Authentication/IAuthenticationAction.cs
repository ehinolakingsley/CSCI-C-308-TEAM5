using CSCI_308_TEAM5.API.Security;

namespace CSCI_308_TEAM5.API.Actions.Authentication
{
    public interface IAuthenticationAction
    {
        Task<IActionResult> authenticate(Roles role, string emailAddress);

        Task<IActionResult> otpValidation(int otpCode);

        Task<IActionResult> logOut();

        Task<IActionResult> addAdmin(UsersArgs args);

        Task<IActionResult> addRider(RiderArgs args);

        Task<IActionResult> addDriver(UsersArgs args);

        Task<IActionResult> getProfileInfo();
    }
}
