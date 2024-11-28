using CSCI_308_TEAM5.API.Services.Config;

namespace CSCI_308_TEAM5.API.Repository.OneTimeCode
{
    interface IOneTimeCodeTb
    {
        Task addOrUpdate(OneTimeTbArgs args);

        Task<OneTimeTbModel> get(int tokenCode);

        Task del(int tokenCode);
    }

    sealed class OneTimeCodeTb(IConfigService configService) : IOneTimeCodeTb
    {
        public Task addOrUpdate(OneTimeTbArgs args)
        {
            throw new NotImplementedException();
        }

        public Task del(int tokenCode)
        {
            throw new NotImplementedException();
        }

        public Task<OneTimeTbModel> get(int tokenCode)
        {
            throw new NotImplementedException();
        }
    }
}
