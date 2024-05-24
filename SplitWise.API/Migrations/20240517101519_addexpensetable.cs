using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SplitWise.API.Migrations
{
    /// <inheritdoc />
    public partial class addexpensetable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Expenses",
                columns: table => new
                {
                    expId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    groupId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Amount = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Currency = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GroupSelection = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Expenses", x => x.expId);
                });

            migrationBuilder.CreateTable(
                name: "ExpensesDetails",
                columns: table => new
                {
                    expdId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    expId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Amount = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Paidby = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ParticipantId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ParticipantAmount = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExpensesDetails", x => x.expdId);
                    table.ForeignKey(
                        name: "FK_ExpensesDetails_Expenses_expId",
                        column: x => x.expId,
                        principalTable: "Expenses",
                        principalColumn: "expId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExpensesDetails_expId",
                table: "ExpensesDetails",
                column: "expId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExpensesDetails");

            migrationBuilder.DropTable(
                name: "Expenses");
        }
    }
}
