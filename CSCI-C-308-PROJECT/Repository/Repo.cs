using CSCI_308_TEAM5.API.Repository.Authentication;
using CSCI_308_TEAM5.API.Repository.GlobalVariable;
using CSCI_308_TEAM5.API.Repository.OneTimeCode;
using CSCI_308_TEAM5.API.Repository.RiderAddress;
using CSCI_308_TEAM5.API.Repository.RiderRequest;
using CSCI_308_TEAM5.API.Repository.Role;
using CSCI_308_TEAM5.API.Repository.Users;

namespace CSCI_308_TEAM5.API.Repository
{
    sealed class Repo(IAuthenticationTb authenticationTb, IRoleTb roleTb, IUsersTb usersTb, IRiderAddressTb riderAddressTb, IOneTimeCodeTb oneTimeCodeTb, IRiderRequestTb riderRequestTb, IGlobalVariableTb globalVariableTb) : IRepo
    {
        public IAuthenticationTb authenticationTb { get; } = authenticationTb;

        public IRoleTb roleTb { get; } = roleTb;

        public IUsersTb usersTb { get; } = usersTb;

        public IRiderAddressTb riderAddressTb { get; } = riderAddressTb;

        public IOneTimeCodeTb oneTimeCodeTb { get; } = oneTimeCodeTb;

        public IRiderRequestTb riderRequestTb { get; } = riderRequestTb;

        public IGlobalVariableTb globalVariableTb { get; } = globalVariableTb;
    }
}
