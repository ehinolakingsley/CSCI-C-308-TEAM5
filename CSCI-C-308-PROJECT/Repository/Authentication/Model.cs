namespace CSCI_308_TEAM5.API.Repository.Authentication
{
    sealed record AuthenticationTbModel
    {
        public Guid UserId { get; set; }

        public string Token { get; set; }

        public Guid RefreshToken { get; set; }

        public DateTime Expires { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime LastAccessed { get; set; }

        public string UserAgent { get; set; }

        public int RoleId { get; set; }
    }
}
