using System;
using System.Collections.Generic;

namespace Team24_BevosBooks.ViewModels
{
    public class OrderReportRowVM
    {
        public int OrderID { get; set; }
        public DateTime OrderDate { get; set; }
        public string CustomerName { get; set; } = "";
        public int TotalQuantity { get; set; }
        public decimal OrderRevenue { get; set; }
        public decimal OrderCost { get; set; }
        public decimal OrderProfit { get; set; }
    }

    public class OrdersReportVM
    {
        public ReportFilterVM Filter { get; set; } = new ReportFilterVM();
        public List<OrderReportRowVM> Rows { get; set; } = new List<OrderReportRowVM>();
        public int RecordCount { get; set; }
    }
}
