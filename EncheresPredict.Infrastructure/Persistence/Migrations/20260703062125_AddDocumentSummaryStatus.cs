using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EncheresPredict.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddDocumentSummaryStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FailureReason",
                table: "DocumentSummaries",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "DocumentSummaries",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FailureReason",
                table: "DocumentSummaries");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "DocumentSummaries");
        }
    }
}
