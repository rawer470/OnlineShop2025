using System;
using Asp2025_Models;

namespace Asp2025_DataAccess.Repository.IRepository;

public interface ICompanyRepository : IRepository<Company>
{
    void Update(Company obj);
}
