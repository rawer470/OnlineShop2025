using System;
using Asp2025_Models;

namespace Asp2025_DataAccess.Repository.IRepository;

public interface IInquiryHeaderRepository : IRepository<InquiryHeader>
{
    void Update(InquiryHeader obj);
}
