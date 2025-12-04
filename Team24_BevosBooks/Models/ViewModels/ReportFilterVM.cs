using System;

namespace Team24_BevosBooks.ViewModels
{
    public class ReportFilterVM
    {
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public int? CustomerId { get; set; }
        public int? BookId { get; set; }
    }
}
