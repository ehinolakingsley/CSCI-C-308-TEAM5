namespace CSCI_308_TEAM5.API.Repository.Users
{
    sealed record UsersTbModel : UsersTbArgs
    {
        public Guid userID { get; set; }

        public DateTime dateCreated { get; set; }

        public DateTime lastModified { get; set; }
    }

    record UsersTbArgs
    {
        public string name { get; set; }

        public string phone { get; set; }

        public string email { get; set; }
    }
}
