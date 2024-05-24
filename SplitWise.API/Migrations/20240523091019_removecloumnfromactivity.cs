using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SplitWise.API.Migrations
{
    /// <inheritdoc />
    public partial class removecloumnfromactivity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TransId",
                table: "Activities");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TransId",
                table: "Activities",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
