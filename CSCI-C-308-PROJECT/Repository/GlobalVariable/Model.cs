namespace CSCI_308_TEAM5.API.Repository.GlobalVariable
{
    public sealed record GlobalVariableTbModel
    {
        public int type { get; set; }

        public string value { get; set; }
    }


    internal enum GlobalVariableType
    {
        DefaultAddress = 0
    }
}
