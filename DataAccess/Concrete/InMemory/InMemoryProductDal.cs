using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess.Concrete.InMemory
{
    public class InMemoryProductDal : IProductDal
    {


        List<Product> _products;

        public InMemoryProductDal()
        {
            _products = new List<Product> {
            
            new Product{ProductId=1,CategoryId=1,ProductName="Bardak",UnitPrice=15,UnitsInStok=15 },
            new Product{ProductId=2,CategoryId=1,ProductName="Kamera",UnitPrice=15333,UnitsInStok=153 } ,
            new Product{ProductId=3,CategoryId=2,ProductName="Telefon",UnitPrice=152,UnitsInStok=152 },
            new Product{ProductId=4,CategoryId=2,ProductName="Klavye",UnitPrice=245,UnitsInStok=34 },
            new Product{ProductId=5,CategoryId=2,ProductName="Mause",UnitPrice=85,UnitsInStok=1 },
              
            };
        }

        public void Add(Product product)
        {
            _products.Add(product);
        }

        public void Delete(Product product)
        {
            Product productToDelete = _products.SingleOrDefault(p=>p.ProductId==product.ProductId);

            _products.Remove(productToDelete);
        }

        public List<Product> GetAll()
        {
            return _products;
        }

        public List<Product> getAllByCategory(int categoryId)
        {
           return _products.Where(p => p.CategoryId == categoryId).ToList();
          
        }

        public void Uptade(Product product)
        {
            Product productToUptade = _products.SingleOrDefault(p => p.ProductId == product.ProductId);

            productToUptade.ProductName = product.ProductName;
            productToUptade.CategoryId = product.CategoryId;
            productToUptade.UnitPrice = product.UnitPrice;
            productToUptade.UnitsInStok = product.UnitsInStok;


        }
    }
}
