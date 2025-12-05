using System.Collections.Generic;

namespace Team24_BevosBooks.ViewModels
{
    public class InventoryRowVM
    {
        public int BookID { get; set; }
        public string Title { get; set; } = "";
        public int InventoryQuantity { get; set; }
        public decimal AverageCost { get; set; }
    }

    public class InventoryReportVM
    {
        public List<InventoryRowVM> Rows { get; set; } = new List<InventoryRowVM>();
        public decimal TotalInventoryValue { get; set; }
        public int RecordCount { get; set; }

        // NEW: Track current sort option
        public string CurrentSort { get; set; } = "title";
    }
}
