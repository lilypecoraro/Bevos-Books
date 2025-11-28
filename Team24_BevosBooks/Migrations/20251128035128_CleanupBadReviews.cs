using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Team24_BevosBooks.Migrations
{
    /// <inheritdoc />
    public partial class CleanupBadReviews : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Delete reviews where ReviewerID does not exist
            migrationBuilder.Sql(@"
        DELETE FROM Reviews
        WHERE ReviewerID NOT IN (SELECT Id FROM AspNetUsers)
           OR (ApproverID IS NOT NULL AND ApproverID NOT IN (SELECT Id FROM AspNetUsers));
    ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // No-op
        }

    }
}
