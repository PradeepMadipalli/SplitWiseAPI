using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SplitWise.API.Migrations
{
    /// <inheritdoc />
    public partial class updatestatuscolumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "UserGroups",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Group",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "UserGroups");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Group");
        }
    }
}
