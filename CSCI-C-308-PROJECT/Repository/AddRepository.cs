namespace CSCI_308_TEAM5.API.Repository
{
    public static class AddRepository
    {
        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IRepo, Repo>();
        }
    }
}
