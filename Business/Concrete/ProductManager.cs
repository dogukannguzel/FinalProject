using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.CCS;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Business.Concrete
{
    public class ProductManager : IProductService
    {


        IProductDal _productDal;
        
        public ProductManager(IProductDal productDal)
        {
            _productDal = productDal;
          
        }

        [SecuredOperation("Product.List,Admin")]
        [ValidationAspect(typeof(ProductValidator))]
        public IResult Add(Product product)
        {
            var result = BusinessRules.Run(CheckIfProductCountOfCategoryCorrect(product.CategoryId), CheckIfProductNameExists(product.ProductName));

            if (!result.Success)
            {
                return result;
            }

           _productDal.Add(product);
           return new SuccessResult(Message.ProductAdded);
            
        }

        public IDataResult<List<Product>> GetAll()
        {
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(),Message.ProductsListed);
        }

        public IDataResult<List<Product>> GetAllCategoryId(int id)
        {
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(p => p.CategoryId == id), Message.ProductsListed); 
        }

        public IDataResult<List<Product>> GetByUnitPrice(decimal min, decimal max)
        {
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(p => p.UnitPrice <= min && p.UnitPrice <= max), Message.ProductsListed); 
        }

        public IDataResult<List<ProductDetailDto>> GetProductDetails()
        {
            return new SuccessDataResult<List<ProductDetailDto>>(_productDal.GetProductDetails(), Message.ProductsListed); 
        }

        private IResult CheckIfProductCountOfCategoryCorrect(int categoryId)
        {
            var result = _productDal.GetAll(p => categoryId == p.CategoryId).Count;
            if (result>=10)
            {
                return new ErrorResult(Message.ProductCountOfCategoryError);
            }
            return new SuccessResult();
        }

        private IResult CheckIfProductNameExists(string productName)
        {
            var result = _productDal.GetAll(p => p.ProductName == productName).Any();

            if (result)
            {
                return new ErrorResult(Message.ProductNameAlreadyExists);
            }
            return new SuccessResult();
        }

    }
}
