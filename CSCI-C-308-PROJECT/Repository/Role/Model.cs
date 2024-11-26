namespace CSCI_308_TEAM5.API.Repository.Role
{
    sealed record RoleTbModel
    {
        public string Role { get; set; }

        public Guid UserId { get; set; }

        public int RoleId { get; set; }
    }
}
