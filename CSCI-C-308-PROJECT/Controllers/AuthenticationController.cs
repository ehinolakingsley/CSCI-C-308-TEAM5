using CSCI_308_TEAM5.API.Actions.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CSCI_308_TEAM5.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthenticationController(IAuthenticationAction authenticationAction) : ControllerBase
    {

        /// <summary>
        /// Authenticate rider's account
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(200, Type = typeof(string))]
        public async Task<IActionResult> auth_rider(string email) => await authenticationAction.authenticate(Security.Roles.Rider, email);

        /// <summary>
        /// Authenticate driver's account
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(200, Type = typeof(string))]
        public async Task<IActionResult> auth_driver(string email) => await authenticationAction.authenticate(Security.Roles.Driver, email);

        /// <summary>
        /// Authenticate user's account
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(200, Type = typeof(string))]
        public async Task<IActionResult> auth_user(string email) => await authenticationAction.authenticate(Security.Roles.None, email);

        /// <summary>
        /// Logout user's active account. User's token must be pass!
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        [ProducesResponseType(200, Type = typeof(string))]
        public async Task<IActionResult> logout() => await authenticationAction.logOut();

        /// <summary>
        /// Rider register or create their account
        /// </summary>
        /// <param name="arg"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [ProducesResponseType(200, Type = typeof(string))]
        public async Task<IActionResult> riderSignUp(RiderArgs arg) => await authenticationAction.addRider(arg);

        /// <summary>
        /// User's registration setup
        /// </summary>
        /// <param name="arg"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [ProducesResponseType(200, Type = typeof(string))]
        public async Task<IActionResult> userSignUp(UserArgs arg) => await authenticationAction.addUser(arg);

    }
}
