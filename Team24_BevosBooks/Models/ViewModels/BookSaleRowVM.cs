using System;
using System.Collections.Generic;

namespace Team24_BevosBooks.ViewModels
{
    public class BookSaleRowVM
    {
        public int BookID { get; set; }
        public string Title { get; set; } = "";
        public int Quantity { get; set; }
        public int OrderID { get; set; }

        // NEW: Customer ID alongside name
        public string CustomerId { get; set; } = "";
        public string CustomerName { get; set; } = "";

        public decimal SellingPrice { get; set; }
        public decimal AverageCost { get; set; }
        public decimal ProfitMargin { get; set; }
        public DateTime OrderDate { get; set; }
    }

    public class BooksSoldReportVM
    {
        public ReportFilterVM Filter { get; set; } = new ReportFilterVM();
        public List<BookSaleRowVM> Rows { get; set; } = new List<BookSaleRowVM>();
        public int RecordCount { get; set; }
        public string CurrentSort { get; set; } = "recent";
    }
}
