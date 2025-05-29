using System;
using Asp2025_DataAccess.Data;
using Asp2025_DataAccess.Repository.IRepository;
using Asp2025_Models;

namespace Asp2025_DataAccess.Repository;

public class CompanyRepository : Repository<Company>, ICompanyRepository
{
    private readonly ApplicationDbContext _db;

    public CompanyRepository(ApplicationDbContext db) : base(db)
    {
        _db = db;
    }

    public void Update(Company obj)
    {
        var objFromDb = _db.Category.FirstOrDefault(u => u.Id == obj.Id);
        if (objFromDb != null)
        {
            objFromDb.Name = obj.Name;
        }
    }
}
