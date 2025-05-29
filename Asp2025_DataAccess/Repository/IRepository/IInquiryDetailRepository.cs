using System;
using Asp2025_Models;

namespace Asp2025_DataAccess.Repository.IRepository;

public interface IInquiryDetailRepository : IRepository<InquiryDetail>
{
    void Update(InquiryDetail obj);
}
