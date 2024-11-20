using CSCI_308_TEAM5.API.Services.Email;

namespace CSCI_308_TEAM5.API.Services
{
    public static class AddService
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddSingleton<IEmailServices, EmailServices>();
        }
    }
}
