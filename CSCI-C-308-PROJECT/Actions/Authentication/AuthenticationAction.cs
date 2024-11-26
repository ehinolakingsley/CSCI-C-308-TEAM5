using CSCI_308_TEAM5.API.Actions.BaseAction;
using CSCI_308_TEAM5.API.Security;
using CSCI_308_TEAM5.API.Services.Email;
using Microsoft.AspNetCore.Mvc;

namespace CSCI_308_TEAM5.API.Actions.Authentication
{
    sealed class AuthenticationAction(IAggregateServices services) : BaseTeam5Action(services), IAuthenticationAction
    {
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

                await repo.roleTb.add(tempUserId.Value, Roles.Rider);

                emailServices.postMail(args.EmailAddress, MailTemplate.welcomeRiderMsg(args.FullName), $"Welcome to {configService.ProductName}");

                return Ok("Rider's account is set up and ready to go");
            });
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
