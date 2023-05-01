using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class ProductAuditLog
    {
        public int Id { get; set; }
        public DateTime EntryDate { get; set; }
        public string Message { get; set; }
        public int ProductId { get; set; }
    }
}
