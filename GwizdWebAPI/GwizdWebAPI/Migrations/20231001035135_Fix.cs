using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GwizdWebAPI.Migrations
{
    /// <inheritdoc />
    public partial class Fix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AnimalImages_DisappearedAnimals_DisappearedAnimalEntityDisa~",
                table: "AnimalImages");

            migrationBuilder.DropForeignKey(
                name: "FK_AnimalImages_FoundedAnimals_FoundedAnimalEntityFoundedAnima~",
                table: "AnimalImages");

            migrationBuilder.AlterColumn<int>(
                name: "FoundedAnimalEntityFoundedAnimalId",
                table: "AnimalImages",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "DisappearedAnimalEntityDisappearedAnimalId",
                table: "AnimalImages",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_AnimalImages_DisappearedAnimals_DisappearedAnimalEntityDisa~",
                table: "AnimalImages",
                column: "DisappearedAnimalEntityDisappearedAnimalId",
                principalTable: "DisappearedAnimals",
                principalColumn: "DisappearedAnimalId");

            migrationBuilder.AddForeignKey(
                name: "FK_AnimalImages_FoundedAnimals_FoundedAnimalEntityFoundedAnima~",
                table: "AnimalImages",
                column: "FoundedAnimalEntityFoundedAnimalId",
                principalTable: "FoundedAnimals",
                principalColumn: "FoundedAnimalId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AnimalImages_DisappearedAnimals_DisappearedAnimalEntityDisa~",
                table: "AnimalImages");

            migrationBuilder.DropForeignKey(
                name: "FK_AnimalImages_FoundedAnimals_FoundedAnimalEntityFoundedAnima~",
                table: "AnimalImages");

            migrationBuilder.AlterColumn<int>(
                name: "FoundedAnimalEntityFoundedAnimalId",
                table: "AnimalImages",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "DisappearedAnimalEntityDisappearedAnimalId",
                table: "AnimalImages",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AnimalImages_DisappearedAnimals_DisappearedAnimalEntityDisa~",
                table: "AnimalImages",
                column: "DisappearedAnimalEntityDisappearedAnimalId",
                principalTable: "DisappearedAnimals",
                principalColumn: "DisappearedAnimalId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AnimalImages_FoundedAnimals_FoundedAnimalEntityFoundedAnima~",
                table: "AnimalImages",
                column: "FoundedAnimalEntityFoundedAnimalId",
                principalTable: "FoundedAnimals",
                principalColumn: "FoundedAnimalId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
