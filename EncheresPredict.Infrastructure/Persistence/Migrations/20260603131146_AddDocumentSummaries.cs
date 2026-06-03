using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EncheresPredict.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddDocumentSummaries : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DocumentSummaries",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AuctionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SummaryJson = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GeneratedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModelVersion = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    PdfUrl = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentSummaries", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DocumentSummaries_AuctionId",
                table: "DocumentSummaries",
                column: "AuctionId");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentSummaries_GeneratedAt",
                table: "DocumentSummaries",
                column: "GeneratedAt");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DocumentSummaries");
        }
    }
}
