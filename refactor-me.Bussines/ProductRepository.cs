using System;
using System.Collections.Generic;
using FluentValidation;
using refactor_me.Bussines.Validator;
using refactor_me.Domain;
using refactor_me.Domain.DataAccessor;
using refactor_me.Domain.Entities;

namespace refactor_me.Bussines
{
    public class ProductRepository: IProductRepository
    {
        private readonly IProductAccessor _productAccessor;

        public ProductRepository(IProductAccessor productAccessor)
        {
            _productAccessor = productAccessor;
        }

        public IEnumerable<Product> GetAll()
        {
            return _productAccessor.GetAll();
        }

        public IEnumerable<Product> GetByName(string name)
        {
            return _productAccessor.GetByName(name);
        }

        public Product GetById(Guid id)
        {
            return _productAccessor.GetById(id);
        }

        public Product Add(Product product)
        {
            new ProductValidator().ValidateAndThrow(product);

            product.Id = _productAccessor.Add(product);

            return product;
        }

        public bool Update(Product product)
        {
            new ProductValidator().ValidateAndThrow(product);

            return _productAccessor.Update(product);
        }

        public bool Delete(Guid id)
        {
            return _productAccessor.Delete(id);
        }
    }
}
