using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.EmailService
{
    public interface IEmailService<T>
    {
        void ProcessEmail();
        Task<T> SendEmailAsync(string email, string subject, string message);
        Task AddEmail(T entity);
    }
}
