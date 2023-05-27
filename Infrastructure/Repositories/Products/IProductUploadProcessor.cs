using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.Products
{
    public interface IProductUploadProcessor
    {
        Task ProcessUploadAsync(string data);
    }
}
