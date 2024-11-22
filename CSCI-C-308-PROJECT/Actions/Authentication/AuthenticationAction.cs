using CSCI_308_TEAM5.API.Actions.BaseAction;

namespace CSCI_308_TEAM5.API.Actions.Authentication
{
    sealed class AuthenticationAction(IAggregateServices services) : BaseTeam5Action(services), IAuthenticationAction
    {

    }
}
