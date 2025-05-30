using System;
using Asp2025_DataAccess.Data;
using Asp2025_DataAccess.Repository.IRepository;
using Asp2025_Models;

namespace Asp2025_DataAccess.Repository;

public class InquiryDetailRepository : Repository<InquiryDetail>, IInquiryDetailRepository
{
    private readonly ApplicationDbContext _db;
    public InquiryDetailRepository(ApplicationDbContext db) : base(db)
    {
        _db = db;
    }
    public void Update(InquiryDetail obj)
    {
        _db.InquiryDetail.Update(obj);
    }
}
