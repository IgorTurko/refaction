using System;
using System.Collections.Generic;
using refactor_me.Domain.Entities;

namespace refactor_me.Domain.DataAccessor
{
    public interface IProductAccessor
    {
        IEnumerable<Product> GetAll();

        IEnumerable<Product> GetByName(string name);

        Product GetById(Guid id);

        Guid Add(Product product);

        bool Update(Product product);

        bool Delete(Guid id);
    }
}
