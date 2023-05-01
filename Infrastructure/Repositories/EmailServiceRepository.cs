using DAL;
using DAL.Models;
using Infrastructure.EmailService;
using System.Data;
using System.Net;
using System.Net.Mail;

namespace Infrastructure.Repositories
{
    public class EmailServiceRepository : GenericEmailService<Email>
    {
        private readonly StoreDBContext context;

        public EmailServiceRepository(StoreDBContext _context) : base(_context)
        {
            context = _context;
        }

        public override void ProcessEmail()
        {
            string email = OrganizationEmailConfiguration.EmailAddress;
            string subject = OrganizationEmailConfiguration.Subject;
            string message = OrganizationEmailConfiguration.Message; //should be from database
            SendEmailAsync(email, subject, message);
        }
        public override async Task<Email> SendEmailAsync(string email, string subject, string message)
        {
            Email emailEntity = new Email();

            using (MailMessage mail = new MailMessage())
            {
                mail.From = new MailAddress(OrganizationEmailConfiguration.EmailAddress);
                mail.To.Add(email); //should be coming from database
                mail.Subject = subject;
                mail.Body = message;
                mail.IsBodyHtml = true;
                //mail.Attachments.Add(new Attachment("C:\\file.zip"));

                //save email
                emailEntity.Subject = subject;
                emailEntity.DateSent = DateTime.Now;
                emailEntity.FromEmail = email;
                emailEntity.Message = message;
                emailEntity.ToEmail = email;

                if (emailEntity != null)
                {
                    await AddEmail(emailEntity);
                }
                //send the email
                using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
                {
                    smtp.EnableSsl = true;
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = new NetworkCredential(OrganizationEmailConfiguration.EmailAddress, OrganizationEmailConfiguration.Password);
                    await smtp.SendMailAsync(mail);
                }
            }
            
            return emailEntity;
        }

        public override async Task AddEmail(Email entity)
        {
            await context.AddAsync(entity);
            SaveChangesAsync();
        }
    }
}
