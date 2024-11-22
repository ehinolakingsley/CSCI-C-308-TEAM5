using Microsoft.AspNetCore.Mvc;

namespace CSCI_308_TEAM5.API.Actions.Authentication
{
    public interface IAuthenticationAction
    {
        Task<IActionResult> AuthenticateAdmin()
    }
}
