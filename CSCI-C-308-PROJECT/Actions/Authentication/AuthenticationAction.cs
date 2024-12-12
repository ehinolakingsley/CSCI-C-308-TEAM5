using CSCI_308_TEAM5.API.Actions.BaseAction;
using CSCI_308_TEAM5.API.Security;
using CSCI_308_TEAM5.API.Services.Email;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CSCI_308_TEAM5.API.Actions.Authentication
{
    sealed class AuthenticationAction(IAggregateServices services) : BaseTeam5Action(services), IAuthenticationAction
    {
        public async Task<IActionResult> addDriver(UsersArgs args)
        {
            return await Execute(async () =>
            {
                var userProfile = await repo.usersTb.get(args.EmailAddress);

                Guid? tempUserId = null;

                if (userProfile is not null)
                {
                    if (await repo.roleTb.any(userProfile.userID, Roles.Driver))
                        return Error(HttpStatusCode.Conflict, "Your driver's account has already created");
                }
                else
                    tempUserId = await repo.usersTb.add(new Repository.Users.UsersTbArgs
                    {
                        email = args.EmailAddress,
                        name = args.FullName
                    });

                await repo.roleTb.add(tempUserId.Value, Roles.Driver, false); // Add driver's permission but deactivate it pending administrator's approval
                emailServices.postMail(args.EmailAddress, MailTemplate.welcomeDriverMsg(args.FullName), "Welcome to Our Driving Community(TheRider) - Account Approval Pending");

                return Ok("Your driver's account has been created, awaiting admin approval.");
            });
        }

        public async Task<IActionResult> addRider(RiderArgs args)
        {
            return await Execute(async () =>
            {
                var userProfile = await repo.usersTb.get(args.EmailAddress);

                var payload = new Repository.Users.UsersTbArgs
                {
                    email = args.EmailAddress,
                    name = args.FullName,
                    phone = args.PhoneNumber,
                };
                Guid? tempUserId = null;

                if (userProfile is not null)
                {
                    if (await repo.roleTb.any(userProfile.userID, Roles.Rider))
                        return Error(HttpStatusCode.Conflict, "Rider's account is already set up and ready to go");

                    // Update profile with new data 
                    await repo.usersTb.update(userProfile.userID, payload);
                    tempUserId = userProfile.userID;
                }
                else
                    tempUserId = await repo.usersTb.add(payload);

                await repo.roleTb.add(tempUserId.Value, Roles.Rider, true);
                var addressId = await repo.riderAddressTb.add(tempUserId.Value, new Repository.RiderAddress.RiderAddressTbArgs
                {
                    city = args.Address.City,
                    country = args.Address.Country,
                    state = args.Address.State,
                    street = args.Address.Street
                });
                await repo.usersTb.updateDefaultAddress(tempUserId.Value, addressId);

                emailServices.postMail(args.EmailAddress, MailTemplate.welcomeRiderMsg(args.FullName), $"Welcome to {configService.ProductName}");

                return Ok("Rider's account is set up and ready to go");
            });
        }

        public async Task<IActionResult> addAdmin(UsersArgs args)
        {
            return await Execute(async () =>
            {
                var userProfile = await repo.usersTb.get(args.EmailAddress);

                Guid? tempUserId = null;

                if (userProfile is not null)
                {
                    if (await repo.roleTb.any(userProfile.userID, Roles.Admin))
                        return Error(HttpStatusCode.Conflict, "Your administrator's account has already been configured.");
                }
                else
                    tempUserId = await repo.usersTb.add(new Repository.Users.UsersTbArgs
                    {
                        email = args.EmailAddress,
                        name = args.FullName
                    });

                await repo.roleTb.add(tempUserId.Value, Roles.Admin, true);
                emailServices.postMail(args.EmailAddress, MailTemplate.welcomeAdminMsg(args.FullName, args.EmailAddress), "Administrator Account Created");

                return Ok("We are pleased to inform you that your administrator account has now been set up and is ready for use.");
            });
        }

        public async Task<IActionResult> authenticate(Roles role, string emailAddress)
        {
            return await Execute(async () =>
            {
                if (emailAddress.empty())
                    return Error(HttpStatusCode.BadRequest, "Your registered email address is required");

                var userProfile = await repo.usersTb.get(emailAddress);

                if (userProfile is null)
                    return Error(HttpStatusCode.Unauthorized, "Authentication failed! Try again with your registered email address.");

                var roleInfo = await repo.roleTb.get(userProfile.userID, role);

                if (roleInfo is null)
                    return Error(HttpStatusCode.Unauthorized, "Authentication failed! Try again with your registered email address.");

                if (!roleInfo.activated)
                    return Error(HttpStatusCode.Unauthorized, "Your account requires attention! Please contact the administrator for assistance.");

                var latestTokenInfo = await repo.authenticationTb.get(userProfile.userID, roleInfo.roleID);

                async Task<IActionResult> manageOTP()
                {
                    // get OneTime token to user
                    int _6DigitsToken = new Random().generateOneTime6DigitCode();

                    await repo.oneTimeCodeTb.addOrUpdate(new Repository.OneTimeCode.OneTimeTbArgs
                    {
                        expires = DateTime.UtcNow.AddMinutes(15),
                        OTP = _6DigitsToken,
                        roleID = roleInfo.roleID,
                        userID = userProfile.userID
                    });

                    // Send out OneTime token to user
                    emailServices.postMail(userProfile.email, MailTemplate.otpMsg(userProfile.name, _6DigitsToken), $"Your Verification Code");

                    // Indicate to user to pop-up OTP form
                    return Error(HttpStatusCode.NoContent, $"OTP code has been sent to {userProfile.email.maskEmail()}. Kindly enter the code into the appropriate field.");
                }

                if (latestTokenInfo is null)
                    return await manageOTP();

                if (DateTime.UtcNow.Subtract(latestTokenInfo.expires) > TimeSpan.FromMinutes(0)) // Existing token has expired
                    return await manageOTP();

                // Send existing token to client
                return Ok(new TokenInfo(latestTokenInfo.refreshToken, latestTokenInfo.token, (int)latestTokenInfo.expires.Subtract(DateTime.UtcNow).TotalSeconds));
            });
        }

        public async Task<IActionResult> getProfileInfo()
        {
            return await Execute(async () =>
            {
                var userProfile = await repo.usersTb.get(userIdentity.userId);

                if (userProfile is null)
                    return Error(HttpStatusCode.NotFound, "Unable to retrieve user's profile information");

                return Ok(new ProfileInfo
                {
                    EmailAddress = userProfile.email.ToLower(),
                    FullName = userProfile.name.formatName(),
                    PhoneNumber = userProfile.phone ?? "N/A"
                });
            });
        }

        public async Task<IActionResult> logOut()
        {
            return await Execute(async () =>
            {
                var userProfile = await repo.usersTb.get(userIdentity.userId);

                if (userProfile is null)
                    return Ok("Session has been logged out...");

                await repo.authenticationTb.del(userIdentity.userId, userIdentity.accessRole);

                return Ok("Session has been logged out...");
            });
        }

        public async Task<IActionResult> otpValidation(int otpCode)
        {
            return await Execute(async () =>
            {
                var otpInfo = await repo.oneTimeCodeTb.get(otpCode);

                if (otpInfo is null)
                    return Error(HttpStatusCode.Unauthorized, "Invalid One-Time Password Provided.");

                if (otpInfo.expires.Subtract(DateTime.UtcNow) < TimeSpan.Zero)
                {
                    await repo.oneTimeCodeTb.del(otpCode);
                    return Error(HttpStatusCode.Unauthorized, "The provided One-Time Password (OTP) has expired.");
                }

                var userProfile = await repo.usersTb.get(userIdentity.userId);

                if (userProfile is null)
                    return Error(HttpStatusCode.Unauthorized, "Unable to complete verification process. Kindly contact administrator for assistance");

                string generateToken(Guid userId, int roleID, string username, DateTime expires)
                {
                    var tokenHandler = new JwtSecurityTokenHandler();
                    List<Claim> claims =
                    [
                        new Claim(Constants.jwtClaimUsername, username),
                        new Claim(Constants.jwtClaimRoleID, roleID.ToString()),
                        new Claim(Constants.jwtClaimUserID, userId.ToString())
                    ];

                    var jwt = new SecurityTokenDescriptor
                    {
                        Subject = new ClaimsIdentity(claims),
                        Issuer = Constants.jwtIssuer,
                        Audience = Constants.jwtAudience,
                        Expires = expires,
                        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configService.jwtSignature)), SecurityAlgorithms.HmacSha256)
                    };

                    return tokenHandler.WriteToken(tokenHandler.CreateToken(jwt));
                }

                // generate token:
                var expires = DateTime.UtcNow.Add(TimeSpan.FromHours(6));
                var token = generateToken(otpInfo.userID, otpInfo.roleID, userProfile.email, expires);
                var refreshToken = Guid.NewGuid();

                await repo.authenticationTb.addOrUpdate(new Repository.Authentication.AuthenticationTbArgs
                {
                    expires = expires,
                    refreshToken = refreshToken,
                    roleId = otpInfo.roleID,
                    token = token,
                    userId = otpInfo.userID,
                });

                await repo.oneTimeCodeTb.del(otpCode);

                return Ok(new TokenInfo(refreshToken, token, (int)expires.Subtract(DateTime.UtcNow).TotalSeconds));
            });
        }

        public async Task<IActionResult> updateRiderProfile(RiderArgs args)
        {
            return await Execute(async () =>
            {
                var userProfile = await repo.usersTb.get(userIdentity.userId);

                if (userProfile is null)
                    return Error(HttpStatusCode.NotFound, "Unable to retrieve account information. Please contact administrator for assistance.");

                var payload = new Repository.Users.UsersTbArgs
                {
                    email = args.EmailAddress,
                    name = args.FullName,
                    phone = args.PhoneNumber,
                };

                // Update profile with new data 
                await repo.usersTb.update(userProfile.userID, payload);

                var addressInfo = new Repository.RiderAddress.RiderAddressTbArgs
                {
                    city = args.Address.City,
                    country = args.Address.Country,
                    state = args.Address.State,
                    street = args.Address.Street
                };

                if (userProfile.defaultAddress.empty())
                {
                    var addressId = await repo.riderAddressTb.add(userProfile.userID, addressInfo);
                    await repo.usersTb.updateDefaultAddress(userProfile.userID, addressId);
                }
                else
                    await repo.riderAddressTb.update(userProfile.defaultAddress.Value, addressInfo);

                return Ok("Your account's profile has been accurately updated");
            });
        }
    }
}
