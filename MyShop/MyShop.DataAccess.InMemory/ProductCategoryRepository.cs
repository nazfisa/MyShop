using MyShop.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.DataAccess.InMemory
{
     public class ProductCategoryRepository
    {
        ObjectCache Cache = MemoryCache.Default;
        List<ProductCategory> ProductCategories = new List<ProductCategory>();

        public ProductCategoryRepository()
        {
           ProductCategories = Cache["ProductCategory"] as List<ProductCategory>;
            if (ProductCategories == null)
            {
                ProductCategories = new List<ProductCategory>();
            }
        }
        public void Commit()
        {
            Cache["ProductCategory"] = ProductCategories;
        }

        public void Insert(ProductCategory p)
        {
            ProductCategories.Add(p);
        }
        public void Update(ProductCategory ProductCategory)
        {
            ProductCategory ProductCategoryToUpdate = ProductCategories.Find(p => p.Id == ProductCategory.Id);
            if (ProductCategoryToUpdate != null)
            {
                ProductCategoryToUpdate = ProductCategory;
            }
            else
            {
                throw new Exception("Product Category No Found");
            }
        }
        public ProductCategory Find(string id)
        {
            ProductCategory productCategory = ProductCategories.Find(p => p.Id == id);
            if (productCategory != null)
            {
                return productCategory;
            }
            else
            {
                throw new Exception("Product Category No Found");
            }
        }
        public IQueryable<ProductCategory> Collection()
        {
            return ProductCategories.AsQueryable();
        }

        public void Delete(string Id)
        {
            ProductCategory productCategoryToDelete = ProductCategories.Find(p => p.Id == Id);
            if (productCategoryToDelete != null)
            {
                ProductCategories.Remove(productCategoryToDelete);
            }
            else
            {
                throw new Exception("Product Category No Found");
            }
        }
    }
}

