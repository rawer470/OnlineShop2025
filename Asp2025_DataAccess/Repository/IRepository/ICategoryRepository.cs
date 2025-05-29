using System;
using Asp2025_Models;

namespace Asp2025_DataAccess.Repository.IRepository;

public interface ICategoryRepository : IRepository<Category>
{
    void Update(Category obj);
}
