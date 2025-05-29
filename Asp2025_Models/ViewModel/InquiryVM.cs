using System;

namespace Asp2025_Models.ViewModel;

public class InquiryVM
{
    public InquiryHeader InquiryHeader { get; set; }
    public IEnumerable<InquiryDetail> InquiryDetails { get; set; }
}
