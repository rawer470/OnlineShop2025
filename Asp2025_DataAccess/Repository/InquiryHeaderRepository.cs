using System;
using Asp2025_DataAccess.Data;
using Asp2025_DataAccess.Repository.IRepository;
using Asp2025_Models;

namespace Asp2025_DataAccess.Repository;

public class InquiryHeaderRepository : Repository<InquiryHeader>, IInquiryHeaderRepository
{
    private readonly ApplicationDbContext _db;
    public InquiryHeaderRepository(ApplicationDbContext db) : base(db)
    {
        _db = db;
    }
    public void Update(InquiryHeader obj)
    {
       _db.InquiryHeader.Update(obj);
    }
} //IInquiryDetailRepository
