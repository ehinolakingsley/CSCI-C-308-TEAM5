using CSCI_308_TEAM5.API.Services.Config;

namespace CSCI_308_TEAM5.API.Repository.GlobalVariable
{
    interface IGlobalVariableTb
    {
        Task<AddressArgs> getDefaultDestinationAddress();
    }

    sealed class GlobalVariableTb(IConfigService configService) : IGlobalVariableTb
    {
    }
}
