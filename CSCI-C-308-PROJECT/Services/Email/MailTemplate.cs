namespace CSCI_308_TEAM5.API.Services.Email
{
    public static class MailTemplate
    {
        public static string welcomeRiderMsg(string name)
        {
            return $"Hi {name.formatName()},\r\n\r\nWelcome aboard! We're thrilled to have you join our community.\r\n\r\nStart exploring and book your first ride today!\r\n\r\nHappy riding,\r\n\r\nCSCI Team5 Support";
        }
    }
}
