using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PustokAB202.Migrations
{
    /// <inheritdoc />
    public partial class NesePropDeleted : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Nese",
                table: "Sliders");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Nese",
                table: "Sliders",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
