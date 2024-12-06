namespace CSCI_308_TEAM5.API.Repository.Authentication
{
    sealed record AuthenticationTbModel : AuthenticationTbArgs
    {
        public DateTime dateCreated { get; set; }
    }

    record AuthenticationTbArgs
    {
        public Guid userId { get; set; }

        public string token { get; set; }

        public Guid refreshToken { get; set; }

        public DateTime expires { get; set; }

        public int roleId { get; set; }
    }
}
