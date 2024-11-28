using Npgsql;
using System.Data.Common;

namespace CSCI_308_TEAM5.API.Services.Config
{
    sealed class ConfigService : IConfigService
    {
        public DeploymentEnv deploymentEnv
        {
            get
            {
                var x = "ASPNETCORE_ENVIRONMENT".getEnvVariable(true);
                return x.CEnum<DeploymentEnv>();
            }
        }

        public DbConnection dbConnection
        {
            get
            {
                string dbPwd = "DB_PWD".getEnvVariable(true);
                string dbHost = "DB_HOST".getEnvVariable(true);
                return new NpgsqlConnection($"User ID=team5;Password={dbPwd};Host={dbHost};Port=5432;Database=TEAM5_API;");
            }
        }

        public string jwtSignature => "JWT_SIGNATURE".getEnvVariable(true);

        public string ProductName => "TheRider";

        public EmailClientCredentialInfo emailClientCredential =>
            new EmailClientCredentialInfo("SMTP_ADDRESS".getEnvVariable(true), "SMTP_PORT".getEnvVariable(true).CInt(), "SMTP_PWD".getEnvVariable(true), "SMTP_SERVER".getEnvVariable(true));
    }
}
