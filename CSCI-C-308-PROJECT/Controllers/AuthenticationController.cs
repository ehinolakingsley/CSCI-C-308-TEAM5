﻿using CSCI_308_TEAM5.API.Actions.Authentication;
using Microsoft.AspNetCore.Authorization;

namespace CSCI_308_TEAM5.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthenticationController(IAuthenticationAction authenticationAction) : ControllerBase
    {

        /// <summary>
        /// Authenticate rider's account
        /// Please NOTE: When a '204 No Content' status code is received, it indicates that an OTP (One-Time Password) is necessary to proceed with the request.
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(200, Type = typeof(TokenInfo))]
        public async Task<IActionResult> auth_rider(string email) => await authenticationAction.authenticate(Security.Roles.Rider, email);

        /// <summary>
        /// Authenticate driver's account
        /// Please NOTE: When a '204 No Content' status code is received, it indicates that an OTP (One-Time Password) is necessary to proceed with the request.
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(200, Type = typeof(TokenInfo))]
        public async Task<IActionResult> auth_driver(string email) => await authenticationAction.authenticate(Security.Roles.Driver, email);

        /// <summary>
        /// Authenticate user's account
        /// Please NOTE: When a '204 No Content' status code is received, it indicates that an OTP (One-Time Password) is necessary to proceed with the request.
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(200, Type = typeof(TokenInfo))]
        public async Task<IActionResult> auth_user(string email) => await authenticationAction.authenticate(Security.Roles.None, email);

        /// <summary>
        /// Logout user's active account. User's token must be pass!
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        [ProducesResponseType(200, Type = typeof(string))]
        public async Task<IActionResult> log_out() => await authenticationAction.logOut();

        /// <summary>
        /// Validate user's token code
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(200, Type = typeof(TokenInfo))]
        public async Task<IActionResult> otp_verification(int code) => await authenticationAction.otpValidation(code);

        /// <summary>
        /// Rider register or create their account
        /// </summary>
        /// <param name="arg"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [ProducesResponseType(200, Type = typeof(string))]
        public async Task<IActionResult> rider_sign_up(RiderArgs arg) => await authenticationAction.addRider(arg);

        /// <summary>
        /// Admin's registration setup
        /// </summary>
        /// <param name="arg"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [ProducesResponseType(200, Type = typeof(string))]
        public async Task<IActionResult> admin_sign_up(UsersArgs arg) => await authenticationAction.addAdmin(arg);

        /// <summary>
        /// Driver's registration setup
        /// </summary>
        /// <param name="arg"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [ProducesResponseType(200, Type = typeof(string))]
        public async Task<IActionResult> driver_sign_up(UsersArgs arg) => await authenticationAction.addDriver(arg);

        /// <summary>
        /// Get user's profile information
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        [ProducesResponseType(200, Type = typeof(ProfileInfo))]
        public async Task<IActionResult> get_profile_info() => await authenticationAction.getProfileInfo();

    }
}
