namespace CSCI_308_TEAM5.API.Repository.OneTimeCode
{
    sealed record OneTimeTbModel : OneTimeTbArgs
    {
        public DateTime dateCreated { get; set; }
    }

    record OneTimeTbArgs
    {
        public Guid userID { get; set; }

        public int roleID { get; set; }

        public int OTP { get; set; }

        public DateTime expires { get; set; }
    }
}
