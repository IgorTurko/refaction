using System;
using System.Collections.Generic;
using System.Linq;
using BLToolkit.Data.Linq;
using refactor_me.Domain.DataAccessor;
using refactor_me.Domain.Entities;

namespace refactor_me.Bussines.DataAccessor
{
    public abstract class ProductOptionAccessor: BLToolkit.DataAccess.DataAccessor, IProductOptionAccessor
    {
        public IEnumerable<ProductOption> GetAll(Guid productId)
        {
            return GetProductOptionQuery()
                .Where(x => x.ProductId == productId)
                .ToList();
        }

        public ProductOption GetById(Guid optionId)
        {
            return GetProductOptionQuery().FirstOrDefault(x => x.Id == optionId);
        }

        public Guid Add(ProductOption productOption)
        {
            return DbManager.SetCommand(@"
                                INSERT INTO ProductOption (
                                    Id, ProductId, Name, Description
                                )
                                OUTPUT inserted.Id
                                VALUES (
                                    NEWID(), @ProductId, @Name, @Description
                                )",
                    DbManager.Parameter("@ProductId", productOption.ProductId),
                    DbManager.Parameter("@Name", productOption.Name),
                    DbManager.Parameter("@Description", productOption.Description))
                .ExecuteScalar<Guid>();
        }

        public bool Update(ProductOption productOption)
        {
            return DbManager.SetCommand(@"
                                UPDATE ProductOption 
                                SET 
                                    Name = @Name
                                    ,Description = @Description
                                WHERE
                                    Id = @Id",
                    DbManager.Parameter("@Id", productOption.Id),
                    DbManager.Parameter("@Name", productOption.Name),
                    DbManager.Parameter("@Description", productOption.Description))
                .ExecuteNonQuery() > 0;
        }

        public bool Delete(Guid optionId)
        {
            return DbManager.SetCommand(@"
                                DELETE FROM ProductOption WHERE Id = @Id",
                           DbManager.Parameter("@Id", optionId))
                       .ExecuteNonQuery() > 0;
        }

        public Table<ProductOption> GetProductOptionQuery()
        {
            return DbManager.GetTable<ProductOption>();
        }
    }
}