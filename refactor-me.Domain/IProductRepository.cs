using System;
using System.Collections.Generic;
using refactor_me.Domain.Entities;

namespace refactor_me.Domain
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetAll();

        IEnumerable<Product> GetByName(string name);

        Product GetById(Guid name);

        Product Add(Product product);

        bool Update(Product product);

        bool Delete(Guid id);
    }
}
