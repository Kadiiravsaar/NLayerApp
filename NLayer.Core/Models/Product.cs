using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Core.Models
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }
        public int Stock { get; set; }
        public decimal Price { get; set; }

        // bire çok ilişki olacağı için
        public int CategoryId { get; set; }// burada bu foregin key olur
        public Category Category { get; set; }

        public ProductFeature ProductFeature { get; set; }
    }
}
