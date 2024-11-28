
namespace CSCI_308_TEAM5.API.Security
{
    public sealed record UserIdentity : IUserIdentity
    {
        public Guid userId { get; set; }

        public string username { get; set; }

        public Roles accessRole { get; set; }

        public string userAgent { get; set; }

        public void update(IUserIdentity identity)
        {
            this.username = identity.username;
            this.accessRole = identity.accessRole;
            this.userId = identity.userId;
            this.userAgent = identity.userAgent;
        }
    }
}
