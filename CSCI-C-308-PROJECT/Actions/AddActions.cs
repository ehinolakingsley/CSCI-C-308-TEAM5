using CSCI_308_TEAM5.API.Actions.Authentication;
using CSCI_308_TEAM5.API.Actions.BaseAction;

namespace CSCI_308_TEAM5.API.Actions
{
    public static class AddAction
    {
        public static void AddActions(this IServiceCollection services)
        {
            services.AddScoped<IAuthenticationAction, AuthenticationAction>();
            services.AddScoped<IAggregateServices, AggregateServices>();
        }
    }
}
