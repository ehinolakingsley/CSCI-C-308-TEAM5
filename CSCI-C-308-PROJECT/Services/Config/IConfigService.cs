using System.Data.Common;

namespace CSCI_308_TEAM5.API.Services.Config
{
    public interface IConfigService
    {
        DeploymentEnv deploymentEnv { get; }

        DbConnection dbConnection { get; }

        string jwtSignature { get; }

        string ProductName { get; }

        EmailClientCredentialInfo emailClientCredential { get; }
    }
}
