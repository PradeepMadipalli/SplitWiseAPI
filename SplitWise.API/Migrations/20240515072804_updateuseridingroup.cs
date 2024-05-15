﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SplitWise.API.Migrations
{
    /// <inheritdoc />
    public partial class updateuseridingroup : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Group",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Group");
        }
    }
}
