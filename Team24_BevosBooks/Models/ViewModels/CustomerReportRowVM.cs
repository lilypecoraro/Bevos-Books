using System.Collections.Generic;

namespace Team24_BevosBooks.ViewModels
{
    public class CustomerReportRowVM
    {
        public string CustomerId { get; set; } = "";
        public string CustomerName { get; set; } = "";
        public int TotalQuantity { get; set; }
        public decimal Revenue { get; set; }
        public decimal Cost { get; set; }
        public decimal Profit { get; set; }
        public decimal Margin { get; set; }

        // NEW: comma-separated distinct book titles purchased by the customer
        public string BookTitles { get; set; } = "";

        // NEW: comma-separated order numbers belonging to the customer
        public string OrderNumbers { get; set; } = "";

        // NEW: weighted averages per unit
        public decimal WeightedAverageRevenue { get; set; }
        public decimal WeightedAverageCost { get; set; }
    }

    public class CustomersReportVM
    {
        public ReportFilterVM Filter { get; set; } = new ReportFilterVM();
        public List<CustomerReportRowVM> Rows { get; set; } = new List<CustomerReportRowVM>();
        public int RecordCount { get; set; }
        public string CurrentSort { get; set; } = "profitDesc";
    }
}
