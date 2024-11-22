using CSCI_308_TEAM5.API.Repository;
using CSCI_308_TEAM5.API.Services.Config;
using CSCI_308_TEAM5.API.Services.Email;

namespace CSCI_308_TEAM5.API.Actions.BaseAction
{
    public interface IAggregateServices
    {
        IRepo repo { get; }

        IConfigService configService { get; }

        IEmailServices emailServices { get; }
    }
}
