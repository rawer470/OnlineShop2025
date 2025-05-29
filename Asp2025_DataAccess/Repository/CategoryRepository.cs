using System;
using Asp2025_DataAccess.Data;
using Asp2025_DataAccess.Repository.IRepository;
using Asp2025_Models;

namespace Asp2025_DataAccess.Repository;

public class CategoryRepository : Repository<Category>, ICategoryRepository
{
private readonly ApplicationDbContext _db;
    public CategoryRepository(ApplicationDbContext db) : base(db)
    {
        _db = db;
    }

    public void Update(Category obj)
    {
       var objFromDb = _db.Category.FirstOrDefault(u => u.Name == obj.Name);
        if (objFromDb != null)
        {
            objFromDb.Name = obj.Name;
            objFromDb.DisplayOrder = obj.DisplayOrder;
        }
    }
}
