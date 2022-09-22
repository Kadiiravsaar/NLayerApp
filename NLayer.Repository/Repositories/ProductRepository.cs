using Microsoft.EntityFrameworkCore;
using NLayer.Core.Models;
using NLayer.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Repository.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        // private readonly AppDbContext _context; burda bunu tanımlamamak için 
        // GenericRepositoryde private olan kısmı protected olarak işaretledim
         
        public ProductRepository(AppDbContext context) : base(context)
        {

        }

        public async Task<List<Product>> GetProductWithCategory()
        {
            // eager loading yaptık yani datayı çekerken kategorilerin de alınması istendi
            // lazy loading kategoriyi de ihtiyaç olduğunda soradan çekersek lazy olur
            return await _context.Products.Include(x => x.Category).ToListAsync();

        }
    }
}
