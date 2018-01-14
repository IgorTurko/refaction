using System;
using System.Collections.Generic;
using refactor_me.Domain.Entities;

namespace refactor_me.Domain
{
    public interface IProductOptionRepository
    {
        IEnumerable<ProductOption> GetAll(Guid productId);

        ProductOption GetById(Guid optionId);

        ProductOption Add(ProductOption productOption);

        bool Update(ProductOption productOption);

        bool Delete(Guid optionId);
    }
}
