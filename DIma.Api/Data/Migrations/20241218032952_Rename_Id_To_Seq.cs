using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dima.Api.Migrations
{
    /// <inheritdoc />
    public partial class Rename_Id_To_Seq : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Category_CategoryId",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_CategoryId",
                table: "Transactions");

            migrationBuilder.RenameColumn(
                name: "CategoryId",
                table: "Transactions",
                newName: "SeqCategory");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Transactions",
                newName: "Seq");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Category",
                newName: "Seq");

            migrationBuilder.AddColumn<long>(
                name: "CategorySeq",
                table: "Transactions",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Category_CategorySeq",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_CategorySeq",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "CategorySeq",
                table: "Transactions");

            migrationBuilder.RenameColumn(
                name: "SeqCategory",
                table: "Transactions",
                newName: "CategoryId");

            migrationBuilder.RenameColumn(
                name: "Seq",
                table: "Transactions",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "Seq",
                table: "Category",
                newName: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_CategoryId",
                table: "Transactions",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Category_CategoryId",
                table: "Transactions",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
