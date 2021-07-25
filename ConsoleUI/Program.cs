using Business.Concrete;
using DataAccess.Concrete.EntityFramework;
using DataAccess.Concrete.InMemory;
using Entities.Concrete;
using System;

namespace ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            //ProductTest();
            // CategoryTest();

            ProductManager productManager = new ProductManager(new EfProductDal());

            foreach (var item in productManager.GetProductDetails().Data)
            {
                Console.WriteLine(item.CategoryName);
            }




        }

        
    }
}
