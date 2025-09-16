using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NonattendanceApp.Migrations
{
    /// <inheritdoc />
    public partial class RemovedToken : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Token",
                table: "Students");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Token",
                table: "Students",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
