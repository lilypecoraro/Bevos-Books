using System.Collections.Generic;

namespace Team24_BevosBooks.ViewModels
{
    public class ReviewsRowVM
    {
        public string? EmployeeId { get; set; }
        public int ApprovedCount { get; set; }
        public int RejectedCount { get; set; }
    }

    public class ReviewsReportVM
    {
        public List<ReviewsRowVM> Rows { get; set; } = new List<ReviewsRowVM>();
        public int RecordCount { get; set; }
    }
}
