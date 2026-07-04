using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EncheresPredict.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class EP102_AlterCreditAccountUserIdToString : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("""
                IF EXISTS (
                    SELECT 1 FROM sys.indexes
                    WHERE name = 'IX_CreditAccounts_UserId'
                      AND object_id = OBJECT_ID('CreditAccounts'))
                DROP INDEX [IX_CreditAccounts_UserId] ON [CreditAccounts];
                """);

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "CreditAccounts",
                type: "nvarchar(450)",
                maxLength: 450,
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.CreateIndex(
                name: "IX_CreditAccounts_UserId",
                table: "CreditAccounts",
                column: "UserId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_CreditAccounts_UserId",
                table: "CreditAccounts");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "CreditAccounts",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldMaxLength: 450);

            migrationBuilder.CreateIndex(
                name: "IX_CreditAccounts_UserId",
                table: "CreditAccounts",
                column: "UserId",
                unique: true);
        }
    }
}
