using DAL;

namespace Infrastructure.EmailService
{
    public abstract class GenericEmailService<T> : IEmailService<T> where T : class
    {
        private readonly StoreDBContext context;

        public GenericEmailService(StoreDBContext _context)
        {
            context = _context;
        }
        public virtual void ProcessEmail()
        {
            throw new NotImplementedException();
        }

        public virtual Task<T> SendEmailAsync(string email, string subject, string message)
        {
            throw new NotImplementedException();
        }

        public void SaveChangesAsync()
        {
            context.SaveChangesAsync();
        }

        public virtual async Task AddEmail(T entity)
        {
             await context.AddAsync(entity);
        }
    }
}
