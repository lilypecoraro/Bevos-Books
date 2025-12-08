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

        // NEW: filters for weighted average cost and profit margin
        public decimal? MinAvgCost { get; set; }
        public decimal? MaxAvgCost { get; set; }
        public decimal? MinProfit { get; set; }
        public decimal? MaxProfit { get; set; }
    }
}
