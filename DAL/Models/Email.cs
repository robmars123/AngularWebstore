using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Email
    {
        public int Id { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
        public string ToEmail { get; set; }
        public string FromEmail { get; set; }
        public DateTime DateSent { get; set; }
    }
}
