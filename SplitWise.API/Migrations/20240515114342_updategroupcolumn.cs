using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SplitWise.API.Migrations
{
    /// <inheritdoc />
    public partial class updategroupcolumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Category",
                table: "Group",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Comments",
                table: "Group",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SimplifyDebts",
                table: "Group",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Category",
                table: "Group");

            migrationBuilder.DropColumn(
                name: "Comments",
                table: "Group");

            migrationBuilder.DropColumn(
                name: "SimplifyDebts",
                table: "Group");
        }
    }
}
