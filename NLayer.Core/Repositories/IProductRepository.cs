using NLayer.Core.DTOs;
using NLayer.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Core.Repositories
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        Task<List<Product>> GetProductWithCategory(); // repositoryler entitiyler ile ilgili olduğu için entitiy(product) döndük
        // fakat serviceler de entity kullanmak zorunda değiliz dto dönebiliriz
    }
}
