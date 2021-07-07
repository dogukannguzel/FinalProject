using Entities.Concrete;
using System.Collections.Generic;

namespace DataAccess.Abstract
{
   public interface IProductDal
    {

        List<Product> GetAll();
        void Add(Product product);
        void Uptade(Product product);
        void Delete(Product product);

        List<Product> getAllByCategory(int categoryId);

    }
}
