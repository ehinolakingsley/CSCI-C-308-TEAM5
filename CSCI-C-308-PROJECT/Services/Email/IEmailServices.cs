namespace CSCI_308_TEAM5.API.Services.Email
{
    interface IEmailServices
    {
        void postMail(string emailAddress, string message, string subject);
    }
}
