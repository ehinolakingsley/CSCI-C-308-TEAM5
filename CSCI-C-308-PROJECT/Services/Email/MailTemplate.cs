namespace CSCI_308_TEAM5.API.Services.Email
{
    public static class MailTemplate
    {
        public static string welcomeRiderMsg(string name)
        {
            return $"Hi {name.formatName()},\r\n\r\nWelcome aboard! We're thrilled to have you join our community.\r\n\r\nStart exploring and book your first ride today!\r\n\r\nHappy riding,\r\n\r\n{Constants.EmailTeamSignature}";
        }

        public static string welcomeDriverMsg(string name)
        {
            return $"Dear {name.formatName()},\r\n\r\nI hope this message finds you well. I am thrilled to welcome you to our community of dedicated drivers! We are confident that your skills and experience will greatly benefit our organization and the riders we serve.\r\n\r\nBefore you can start logging in and helping us pick up riders, there is one important step left: account approval. Our administration team is currently reviewing your application to ensure that all necessary information has been provided and that you meet our community standards. We kindly ask for your patience during this process.\r\n\r\nOnce your account has been approved, we will send you a follow-up email with instructions on how to log in and get started. In the meantime, please feel free to reach out if you have any questions or concerns. We are always here to help!\r\n\r\nThank you for choosing to join our community of drivers. We look forward to working with you soon.\r\n\r\nBest regards,\r\n\r\n{Constants.EmailTeamSignature}";
        }

        public static string welcomeAdminMsg(string name, string username)
        {
            return $"Dear {name.formatName()},\r\n\r\nI hope this message finds you well. I am pleased to inform you that your new administrator account has been successfully created! We are excited to have you on board and confident that your contributions will be invaluable to our organization.\r\n\r\nEnclosed, please find the login information for your new account:\n\nUsername: <strong>{username}</strong>\n\nOnce logged in, you will have access to various administrative features and tools that will help you manage our community of drivers and riders more efficiently. We encourage you to explore these resources and let us know if you have any questions or need further assistance.\r\n\r\nThank you for choosing to be an administrator with us. We look forward to working together to build a thriving and supportive community.\r\n\r\nBest regards,\r\n\r\n{Constants.EmailTeamSignature}";
        }

        public static string otpMsg(string name, int otp)
        {
            return $"Dear {name},\r\n\r\nHere’s your One-Time Password (OTP) for account verification:\r\n\r\nOTP: {otp}\r\n\r\nPlease enter this code on the sign-in page to verify your identity. The code is valid for 15 minutes only.\r\n\r\nFor support, contact us at support@therider.com or call (317) 123-4567.\r\n\r\n{Constants.EmailTeamSignature}";
        }
    }
}
