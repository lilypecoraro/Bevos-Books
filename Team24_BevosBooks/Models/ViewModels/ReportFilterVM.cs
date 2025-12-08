using System;

namespace Team24_BevosBooks.ViewModels
{
    public class ReportFilterVM
    {
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }

        // REPLACED: name-based filtering fields
        public string? CustomerName { get; set; }
        public string? BookName { get; set; }

        // NEW: filters for weighted average cost and profit margin
        public decimal? MinAvgCost { get; set; }
        public decimal? MaxAvgCost { get; set; }
        public decimal? MinProfit { get; set; }
        public decimal? MaxProfit { get; set; }

        // NEW: per-order revenue filters
        public decimal? MinRevenue { get; set; }
        public decimal? MaxRevenue { get; set; }
    }
}
