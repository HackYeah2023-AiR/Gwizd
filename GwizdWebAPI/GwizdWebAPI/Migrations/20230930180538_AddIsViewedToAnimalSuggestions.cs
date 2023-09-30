using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GwizdWebAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddIsViewedToAnimalSuggestions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsViewed",
                table: "AnimalSuggestions",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsViewed",
                table: "AnimalSuggestions");
        }
    }
}
