using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartQueue.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddDisplayNumber : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DisplayNumber",
                table: "Tickets",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "SequenceNumber",
                table: "Tickets",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ServiceCode",
                table: "Tickets",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DisplayNumber",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "SequenceNumber",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "ServiceCode",
                table: "Tickets");
        }
    }
}
