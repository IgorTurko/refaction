using System;
using System.Collections.Generic;
using System.Linq;
using BLToolkit.Data.Linq;
using refactor_me.Domain.DataAccessor;
using refactor_me.Domain.Entities;

namespace refactor_me.Bussines.DataAccessor
{
    public abstract class ProductAccessor: BLToolkit.DataAccess.DataAccessor, IProductAccessor
    {
        public IEnumerable<Product> GetAll()
        {
            return GetProductQuery().ToList();
        }

        public IEnumerable<Product> GetByName(string name)
        {
            return GetProductQuery()
                .Where(x => x.Name.Contains(name))
                .ToList();
        }

        public Product GetById(Guid id)
        {
            return GetProductQuery().FirstOrDefault(x => x.Id == id);
        }

        public Guid Add(Product product)
        {
            return DbManager.SetCommand(@"
                                INSERT INTO Product (
                                    Id, Name, Description, Price, DeliveryPrice
                                )
                                OUTPUT inserted.Id
                                VALUES (
                                    NEWID(), @Name, @Description, @Price, @DeliveryPrice
                                )",
                    DbManager.Parameter("@Name", product.Name),
                    DbManager.Parameter("@Description", product.Description),
                    DbManager.Parameter("@Price", product.Price),
                    DbManager.Parameter("@DeliveryPrice", product.DeliveryPrice))
                .ExecuteScalar<Guid>();
        }

        public bool Update(Product product)
        {
            return DbManager.SetCommand(@"
                                UPDATE Product 
                                SET 
                                    Name = @Name
                                    ,Description = @Description
                                    ,Price = @Price
                                    ,DeliveryPrice = @DeliveryPrice
                                WHERE
                                    Id = @Id",
                    DbManager.Parameter("@Id", product.Id),
                    DbManager.Parameter("@Name", product.Name),
                    DbManager.Parameter("@Description", product.Description),
                    DbManager.Parameter("@Price", product.Price),
                    DbManager.Parameter("@DeliveryPrice", product.DeliveryPrice))
                .ExecuteNonQuery() > 0;
        }

        public bool Delete(Guid id)
        {
            return DbManager.SetCommand(@"
                                BEGIN transaction;
                                DELETE FROM Product WHERE Id = @Id
                                DELETE FROM ProductOption WHERE ProductId = @Id
                                COMMIT transaction;",
                           DbManager.Parameter("@Id", id))
                       .ExecuteNonQuery() > 0;
        }

        public Table<Product> GetProductQuery()
        {
            return DbManager.GetTable<Product>();
        }
    }
}