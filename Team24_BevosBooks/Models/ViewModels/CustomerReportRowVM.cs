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
    }

    public class CustomersReportVM
    {
        public ReportFilterVM Filter { get; set; } = new ReportFilterVM();
        public List<CustomerReportRowVM> Rows { get; set; } = new List<CustomerReportRowVM>();
        public int RecordCount { get; set; }

        // NEW: Track current sort option
        public string CurrentSort { get; set; } = "profitDesc";
       

    }
}
