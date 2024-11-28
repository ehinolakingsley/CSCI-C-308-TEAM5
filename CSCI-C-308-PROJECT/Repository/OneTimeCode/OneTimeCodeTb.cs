using CSCI_308_TEAM5.API.Services.Config;

namespace CSCI_308_TEAM5.API.Repository.OneTimeCode
{
    interface IOneTimeCodeTb
    {
        Task addOrUpdate(OneTimeTbArgs args);
    }

    sealed class OneTimeCodeTb(IConfigService configService) : IOneTimeCodeTb
    {
    }
}
