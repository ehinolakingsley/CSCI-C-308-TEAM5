﻿using CSCI_308_TEAM5.API.Repository.Authentication;
using CSCI_308_TEAM5.API.Repository.GlobalVariable;
using CSCI_308_TEAM5.API.Repository.OneTimeCode;
using CSCI_308_TEAM5.API.Repository.RiderAddress;
using CSCI_308_TEAM5.API.Repository.RiderRequest;
using CSCI_308_TEAM5.API.Repository.Role;
using CSCI_308_TEAM5.API.Repository.Users;

namespace CSCI_308_TEAM5.API.Repository
{
    public static class AddRepository
    {
        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IRepo, Repo>();
            services.AddScoped<IAuthenticationTb, AuthenticationTb>();
            services.AddScoped<IRoleTb, RoleTb>();
            services.AddScoped<IUsersTb, UsersTb>();
            services.AddScoped<IOneTimeCodeTb, OneTimeCodeTb>();
            services.AddScoped<IRiderAddressTb, RiderAddressTb>();
            services.AddScoped<IGlobalVariableTb, GlobalVariableTb>();
            services.AddScoped<IRiderAddressTb, RiderAddressTb>();
            services.AddScoped<IRiderRequestTb, RiderRequestTb>();
        }
    }
}
