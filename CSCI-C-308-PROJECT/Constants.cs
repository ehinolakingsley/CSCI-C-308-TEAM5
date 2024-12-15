namespace CSCI_308_TEAM5.API
{
    public struct Constants
    {
        public const string EmailTeamSignature = "CSCI Team5 Support\nTheRiders";

        public const string jwtIssuer = "csci-team5-members";
        public const string jwtAudience = "Transportation Users";

        public const string jwtClaimUsername = "username";
        public const string jwtClaimRoleID = "role";
        public const string jwtClaimUserID = "id";

        public const string ErrNotFound = "Sorry, can't validate account. Contact administrator for assistance.";

    }
}
