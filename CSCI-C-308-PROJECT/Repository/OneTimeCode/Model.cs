namespace CSCI_308_TEAM5.API.Repository.OneTimeCode
{
    sealed record OneTimeTbModel : OneTimeTbArgs
    {
        public DateTime DateCreated { get; set; }
    }

    record OneTimeTbArgs
    {
        public Guid UserId { get; set; }

        public int RoleId { get; set; }

        public int OTP { get; set; }

        public DateTime Expires { get; set; }
    }
}
