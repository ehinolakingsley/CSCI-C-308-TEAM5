using CSCI_308_TEAM5.API.Actions.BaseAction;
using CSCI_308_TEAM5.API.Security;
using CSCI_308_TEAM5.API.Services.Email;

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
                    if (await repo.roleTb.any(userProfile.UserId, Roles.Driver))
                        return Error(HttpStatusCode.Conflict, "Your driver's account has already been configured.");
                }
                else
                    tempUserId = await repo.usersTb.add(new Repository.Users.UsersTbArgs
                    {
                        Email = args.EmailAddress,
                        Name = args.FullName
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
                    Email = args.EmailAddress,
                    Name = args.FullName,
                    Phone = args.PhoneNumber,
                };
                Guid? tempUserId = null;

                if (userProfile is not null)
                {
                    if (await repo.roleTb.any(userProfile.UserId, Roles.Rider))
                        return Error(HttpStatusCode.Conflict, "Rider's account is already set up and ready to go");

                    // Update profile with new data 
                    await repo.usersTb.update(userProfile.UserId, payload);
                    tempUserId = userProfile.UserId;
                }
                else
                    tempUserId = await repo.usersTb.add(payload);

                await repo.roleTb.add(tempUserId.Value, Roles.Rider, true);
                await repo.riderAddressTb.add(tempUserId.Value, new Repository.RiderAddress.RiderAddressTbArgs
                {
                    City = args.Address.City,
                    Country = args.Address.Country,
                    State = args.Address.State,
                    Street = args.Address.Street
                });

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
                    if (await repo.roleTb.any(userProfile.UserId, Roles.Admin))
                        return Error(HttpStatusCode.Conflict, "Your administrator's account has already been configured.");
                }
                else
                    tempUserId = await repo.usersTb.add(new Repository.Users.UsersTbArgs
                    {
                        Email = args.EmailAddress,
                        Name = args.FullName
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

                var roleInfo = await repo.roleTb.get(userProfile.UserId, role);

                if (roleInfo is null)
                    return Error(HttpStatusCode.Unauthorized, "Authentication failed! Try again with your registered email address.");

                if (!roleInfo.Activated)
                    return Error(HttpStatusCode.Unauthorized, "Your account requires attention! Please contact the administrator for assistance.");

                var latestTokenInfo = await repo.authenticationTb.get(userProfile.UserId, roleInfo.RoleId);

                async Task<IActionResult> manageOTP()
                {
                    // get OneTime token to user
                    int _6DigitsToken = new Random().generateOneTime6DigitCode();

                    await repo.oneTimeCodeTb.addOrUpdate(new Repository.OneTimeCode.OneTimeTbArgs
                    {
                        Expires = DateTime.UtcNow.AddMinutes(15),
                        OTP = _6DigitsToken,
                        RoleId = roleInfo.RoleId,
                        UserAgent = userIdentity.userAgent,
                        UserId = userProfile.UserId
                    });

                    // Send out OneTime token to user
                    emailServices.postMail(userProfile.Email, MailTemplate.otpMsg(userProfile.Name, _6DigitsToken), $"Your Verification Code");

                    // Indicate to user to pop-up OTP form
                    return Error(HttpStatusCode.NoContent, $"OTP code has been sent to {userProfile.Email.maskEmail()}. Kindly enter the code into the appropriate field.");
                }

                if (latestTokenInfo is null)
                    return await manageOTP();

                if (DateTime.UtcNow.Subtract(latestTokenInfo.Expires) > TimeSpan.FromMinutes(0)) // Existing token has expired
                    return await manageOTP();

                // Send existing token to client
                return Ok(new TokenInfo(latestTokenInfo.RefreshToken, latestTokenInfo.Token, (int)latestTokenInfo.Expires.Subtract(DateTime.UtcNow).TotalSeconds));
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
                    EmailAddress = userProfile.Email.ToLower(),
                    FullName = userProfile.Name.formatName(),
                    PhoneNumber = userProfile.Phone ?? "N/A"
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

        public async Task<IActionResult> otpValidation(string otpCode)
        {
            return await Execute(async () =>
            {


            });
        }
    }
}
