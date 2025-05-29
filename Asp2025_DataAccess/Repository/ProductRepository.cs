using System;
using Asp2025_DataAccess.Data;
using Asp2025_DataAccess.Repository.IRepository;
using Asp2025_Models;

namespace Asp2025_DataAccess.Repository;

public class ProductRepository : Repository<Product>, IProductRepository
{
    private readonly ApplicationDbContext _db;
    public ProductRepository(ApplicationDbContext db) : base(db)
    {
        _db = db;
    }
    public void Update(Product obj)
    {
        var objFromDb = _db.Products.FirstOrDefault(u => u.Id == obj.Id);
        if (objFromDb != null)
        {
            objFromDb.Name = obj.Name;
            objFromDb.Price = obj.Price;
            objFromDb.Description = obj.Description;
            objFromDb.CategoryId = obj.CategoryId;
            objFromDb.CompanyId = obj.CompanyId;
            if (obj.Image != null)
            {
                objFromDb.Image = obj.Image;
            }

        }
    }
}
