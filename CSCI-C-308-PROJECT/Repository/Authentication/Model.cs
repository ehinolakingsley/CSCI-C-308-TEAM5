namespace CSCI_308_TEAM5.API.Repository.Authentication
{
    sealed record AuthenticationTbModel : AuthenticationTbArgs
    {
        public DateTime DateCreated { get; set; }
    }

    record AuthenticationTbArgs
    {
        public Guid UserId { get; set; }

        public string Token { get; set; }

        public Guid RefreshToken { get; set; }

        public DateTime Expires { get; set; }

        public int RoleId { get; set; }
    }
}
