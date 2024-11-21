namespace CSCI_308_TEAM5.API.Security
{
    public interface IUserIdentity
    {
        Guid userId { get; }

        string username { get; }

        Roles accessRole { get; }

        void update(IUserIdentity identity);
    }
}
