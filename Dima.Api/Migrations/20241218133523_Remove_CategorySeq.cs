using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dima.Api.Migrations
{
    /// <inheritdoc />
    public partial class Remove_CategorySeq : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Category_CategorySeq",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_CategorySeq",
                table: "Transactions");

            migrationBuilder.RenameColumn(
                name: "CategorySeq",
                table: "Transactions",
                newName: "SeqCategory");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Category_SeqCategory",
                table: "Transactions",
                column: "SeqCategory",
                principalTable: "Category",
                principalColumn: "Seq",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Category_SeqCategory",
                table: "Transactions");

            migrationBuilder.RenameColumn(
                name: "SeqCategory",
                table: "Transactions",
                newName: "CategorySeq");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_CategorySeq",
                table: "Transactions",
                column: "CategorySeq");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Category_CategorySeq",
                table: "Transactions",
                column: "CategorySeq",
                principalTable: "Category",
                principalColumn: "Seq",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
