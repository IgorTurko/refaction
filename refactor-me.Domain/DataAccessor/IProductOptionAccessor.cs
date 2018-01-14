using System;
using System.Collections.Generic;
using refactor_me.Domain.Entities;

namespace refactor_me.Domain.DataAccessor
{
    public interface IProductOptionAccessor
    {
        IEnumerable<ProductOption> GetAll(Guid productId);

        ProductOption GetById(Guid optionId);

        Guid Add(ProductOption productOption);

        bool Update(ProductOption productOption);

        bool Delete(Guid optionId);
    }
}
