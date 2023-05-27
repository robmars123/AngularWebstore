using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace DAL.DTO
{
    public class FileModel
    {
        public string fileName { get; set; }
        public IFormFile file { get; set; }
    }
}
