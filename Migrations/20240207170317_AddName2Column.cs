using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TechChallengue.Migrations
{
    /// <inheritdoc />
    public partial class AddName2Column : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name2",
                table: "Player",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name2",
                table: "Player");
        }
    }
}
