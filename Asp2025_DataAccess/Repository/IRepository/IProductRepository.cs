using System;
using Asp2025_Models;

namespace Asp2025_DataAccess.Repository.IRepository;

public interface IProductRepository : IRepository<Product>
{
    void Update(Product obj);
}
