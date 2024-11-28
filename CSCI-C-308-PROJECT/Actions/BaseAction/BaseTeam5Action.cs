namespace CSCI_308_TEAM5.API.Actions.BaseAction
{
    abstract class BaseTeam5Action(IAggregateServices services) :
        AggregateServices(services.repo, services.configService, services.emailServices, services.userIdentity);
}
