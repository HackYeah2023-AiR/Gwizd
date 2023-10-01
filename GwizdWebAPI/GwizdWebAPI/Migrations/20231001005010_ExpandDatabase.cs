using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GwizdWebAPI.Migrations
{
    /// <inheritdoc />
    public partial class ExpandDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FoundedAnimals_Users_ReporterId",
                table: "FoundedAnimals");

            migrationBuilder.DropColumn(
                name: "Location",
                table: "FoundedAnimals");

            migrationBuilder.DropColumn(
                name: "DisapperanceLocation",
                table: "DisappearedAnimals");

            migrationBuilder.RenameColumn(
                name: "DisappearanceDate",
                table: "DisappearedAnimals",
                newName: "Date");

            migrationBuilder.AlterColumn<int>(
                name: "ReporterId",
                table: "FoundedAnimals",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<double>(
                name: "Latitude",
                table: "FoundedAnimals",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Longitude",
                table: "FoundedAnimals",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Latitude",
                table: "DisappearedAnimals",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Longitude",
                table: "DisappearedAnimals",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddForeignKey(
                name: "FK_FoundedAnimals_Users_ReporterId",
                table: "FoundedAnimals",
                column: "ReporterId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FoundedAnimals_Users_ReporterId",
                table: "FoundedAnimals");

            migrationBuilder.DropColumn(
                name: "Latitude",
                table: "FoundedAnimals");

            migrationBuilder.DropColumn(
                name: "Longitude",
                table: "FoundedAnimals");

            migrationBuilder.DropColumn(
                name: "Latitude",
                table: "DisappearedAnimals");

            migrationBuilder.DropColumn(
                name: "Longitude",
                table: "DisappearedAnimals");

            migrationBuilder.RenameColumn(
                name: "Date",
                table: "DisappearedAnimals",
                newName: "DisappearanceDate");

            migrationBuilder.AlterColumn<int>(
                name: "ReporterId",
                table: "FoundedAnimals",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Location",
                table: "FoundedAnimals",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DisapperanceLocation",
                table: "DisappearedAnimals",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_FoundedAnimals_Users_ReporterId",
                table: "FoundedAnimals",
                column: "ReporterId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
