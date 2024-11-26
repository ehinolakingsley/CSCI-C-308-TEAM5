namespace CSCI_308_TEAM5.API.Repository.Users
{
    sealed record UsersTbModel : UsersTbArgs
    {
        public Guid UserId { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime LastModified { get; set; }
    }

    record UsersTbArgs
    {
        public string Name { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }
    }
}
