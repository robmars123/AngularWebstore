namespace Infrastructure.Repositories.EmailService
{
    public static class OrganizationEmailConfiguration
    {
        //below should be coming from database
        private static readonly string _emailAddress = "robmars2008test@gmail.com";
        private static readonly string _message = "<h1>Upload Successful</h1>";
        private static readonly string _subject = "Upload Successful";
        private static readonly string _password = "leohgzzsmffdixps";
        public static string EmailAddress
        {
            get
            {
                return _emailAddress;
            }
            set
            {
            }
        }
        public static string Message
        {
            get
            {
                return _message;
            }
            set
            {
            }
        }
        public static string Subject
        {
            get
            {
                return _subject;
            }
            set
            {
            }
        }
        public static string Password
        {
            get
            {
                return _password;
            }
            set
            {
            }
        }
    }
}
