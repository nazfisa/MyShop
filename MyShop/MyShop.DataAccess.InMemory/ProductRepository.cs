using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Caching;
using MyShop.Core.Models;

namespace MyShop.DataAccess.InMemory
{
    public class ProductRepository
    {
        ObjectCache Cache = MemoryCache.Default;
        List<Product> products = new List<Product>();

        public ProductRepository()
        {
            products = Cache["products"] as List<Product>;
            if(products== null)
            {
                products = new List<Product>();
            }
        }
        public void Commit()
        {
            Cache["products"] = products;
        }

        public void Insert(Product p)
        {
            products.Add(p);
        }
        public void Update(Product product)
        {
            Product ProductToUpdate = products.Find(p => p.Id == product.Id);
            if(ProductToUpdate!=null)
            {
                ProductToUpdate = product;
            }
            else
            {
                throw new Exception("Product No Found");
            }
        }
        public Product Find(string id)
        {
            Product product = products.Find(p => p.Id == id);
            if (product != null)
            {
                return product;
            }
            else
            {
                throw new Exception("Product No Found");
            }
        }
        public IQueryable<Product> Collection() {
            return products.AsQueryable();
        }

        public void Delete (string Id)
        {
            Product productToDelete = products.Find(p => p.Id == Id);
            if (productToDelete != null)
            {
                products.Remove(productToDelete);
            }
            else
            {
                throw new Exception("Product No Found");
            }
        }
    }
}
