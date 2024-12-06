namespace CSCI_308_TEAM5.API.Repository.Role
{
    sealed record RoleTbModel
    {
        public string role { get; set; }

        public Guid userID { get; set; }

        public int roleID { get; set; }

        public bool activated { get; set; }
    }
}
