﻿using CSCI_308_TEAM5.API.Repository.Authentication;
using CSCI_308_TEAM5.API.Repository.GlobalVariable;
using CSCI_308_TEAM5.API.Repository.OneTimeCode;
using CSCI_308_TEAM5.API.Repository.RiderAddress;
using CSCI_308_TEAM5.API.Repository.RiderRequest;
using CSCI_308_TEAM5.API.Repository.Role;
using CSCI_308_TEAM5.API.Repository.Users;

namespace CSCI_308_TEAM5.API.Repository
{
    interface IRepo
    {
        IAuthenticationTb authenticationTb { get; }

        IRoleTb roleTb { get; }

        IUsersTb usersTb { get; }

        IRiderAddressTb riderAddressTb { get; }

        IOneTimeCodeTb oneTimeCodeTb { get; }

        IRiderRequestTb riderRequestTb { get; }

        IGlobalVariableTb globalVariableTb { get; }
    }
}
