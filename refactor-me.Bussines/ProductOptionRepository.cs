using System;
using System.Collections.Generic;
using FluentValidation;
using refactor_me.Bussines.Validator;
using refactor_me.Domain;
using refactor_me.Domain.DataAccessor;
using refactor_me.Domain.Entities;

namespace refactor_me.Bussines
{
    public class ProductOptionRepository: IProductOptionRepository
    {
        private readonly IProductOptionAccessor _productOptionAccessor;

        public ProductOptionRepository(IProductOptionAccessor productOptionAccessor)
        {
            _productOptionAccessor = productOptionAccessor;
        }

        public IEnumerable<ProductOption> GetAll(Guid productId)
        {
            return _productOptionAccessor.GetAll(productId);
        }

        public ProductOption GetById(Guid optionId)
        {
            return _productOptionAccessor.GetById(optionId);
        }

        public ProductOption Add(ProductOption productOption)
        {
            new ProductOptionValidator().ValidateAndThrow(productOption);

            productOption.Id = _productOptionAccessor.Add(productOption);

            return productOption;
        }

        public bool Update(ProductOption productOption)
        {
            new ProductOptionValidator().ValidateAndThrow(productOption);

            return _productOptionAccessor.Update(productOption);
        }

        public bool Delete(Guid optionId)
        {
            return _productOptionAccessor.Delete(optionId);
        }
    }
}
