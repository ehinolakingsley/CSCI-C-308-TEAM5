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

        public static string riderRequestMsg(string driverName, string pickUpLocation, string dropOffLocation, string rideAcknowledgmentLink, string pickUpDateTime, string riderName)
        {
            return $"Dear {driverName},\n\nA new ride request has just come in, and we're counting on your expertise.\n\n\n{"Request Details:".boldHTMLTxt()}\n{"Pickup Location:".boldHTMLTxt()} {pickUpLocation.linkHTMLTxt()}\n{"Dropoff Location:".boldHTMLTxt()} {dropOffLocation.linkHTMLTxt()}\n\n\nPlease confirm your availability to accept this trip by clicking on the acknowledgment link below.\n\n\n{"Key Information:".boldHTMLTxt()}\n{"Rider's Name:".boldHTMLTxt()} {riderName.Titleize()}\n{"Estimated Pick-up Time:".boldHTMLTxt()} {pickUpDateTime}\n{"Acknowledgd Pick-up:".boldHTMLTxt()} {rideAcknowledgmentLink.linkHTMLTxt()}\n\n\nThank you for your prompt response and dedication. Let's get this ride underway!\r\n\r\nBest regards,\r\n\r\n{Constants.EmailTeamSignature}";
        }

        public static string rideCancellationMsg(string driverName, string pickUpLocation, string pickUpDateTime, string riderName)
        {
            return $"Dear {driverName},\n\nWe regret to inform you that the passenger has decided to terminate their ride request.\n\n\n{"Details of Cancelled Trip:".boldHTMLTxt()}\n{"Original  Pickup Location:".boldHTMLTxt()} {pickUpLocation.linkHTMLTxt()}\n{"Original Pickup Time:".boldHTMLTxt()} {pickUpDateTime.linkHTMLTxt()}\n\n\nThank you for being available and ready to provide transportation services to our community of riders. Your dedication and commitment to excellence do not go unnoticed, and we appreciate your continued support in helping us make ride sharing safe, convenient, and reliable for everyone involved.\n\nIf you have any questions about this cancellation or if there is anything else we can assist you with, please don't hesitate to contact us at any time. We are always here to help!\r\n\r\nBest regards,\r\n\r\n{Constants.EmailTeamSignature}";
        }



        public static string boldHTMLTxt(this string text) => $"<strong>{text}</strong>";
        public static string linkHTMLTxt(this string text) => $"<a>{text}</a>";
    }
}
