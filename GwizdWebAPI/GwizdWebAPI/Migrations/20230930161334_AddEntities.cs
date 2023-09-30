using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace GwizdWebAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "Users",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "DisappearedAnimals",
                columns: table => new
                {
                    DisappearedAnimalId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DisapperanceLocation = table.Column<string>(type: "text", nullable: false),
                    DisappearanceDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    SpeciesName = table.Column<string>(type: "text", nullable: false),
                    OwnerId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DisappearedAnimals", x => x.DisappearedAnimalId);
                    table.ForeignKey(
                        name: "FK_DisappearedAnimals_Users_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "FoundedAnimals",
                columns: table => new
                {
                    FoundedAnimalId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Location = table.Column<string>(type: "text", nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    SpeciesName = table.Column<string>(type: "text", nullable: false),
                    ReporterId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FoundedAnimals", x => x.FoundedAnimalId);
                    table.ForeignKey(
                        name: "FK_FoundedAnimals_Users_ReporterId",
                        column: x => x.ReporterId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AnimalImages",
                columns: table => new
                {
                    AnimalImageId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ImageBlob = table.Column<byte[]>(type: "bytea", nullable: false),
                    DisappearedAnimalEntityDisappearedAnimalId = table.Column<int>(type: "integer", nullable: true),
                    FoundedAnimalEntityFoundedAnimalId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnimalImages", x => x.AnimalImageId);
                    table.ForeignKey(
                        name: "FK_AnimalImages_DisappearedAnimals_DisappearedAnimalEntityDisa~",
                        column: x => x.DisappearedAnimalEntityDisappearedAnimalId,
                        principalTable: "DisappearedAnimals",
                        principalColumn: "DisappearedAnimalId");
                    table.ForeignKey(
                        name: "FK_AnimalImages_FoundedAnimals_FoundedAnimalEntityFoundedAnima~",
                        column: x => x.FoundedAnimalEntityFoundedAnimalId,
                        principalTable: "FoundedAnimals",
                        principalColumn: "FoundedAnimalId");
                });

            migrationBuilder.CreateTable(
                name: "AnimalSuggestions",
                columns: table => new
                {
                    AnimalSuggestionID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Similarity = table.Column<double>(type: "double precision", nullable: false),
                    DisappearedAnimalId = table.Column<int>(type: "integer", nullable: true),
                    FoundedAnimalId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnimalSuggestions", x => x.AnimalSuggestionID);
                    table.ForeignKey(
                        name: "FK_AnimalSuggestions_DisappearedAnimals_DisappearedAnimalId",
                        column: x => x.DisappearedAnimalId,
                        principalTable: "DisappearedAnimals",
                        principalColumn: "DisappearedAnimalId");
                    table.ForeignKey(
                        name: "FK_AnimalSuggestions_FoundedAnimals_FoundedAnimalId",
                        column: x => x.FoundedAnimalId,
                        principalTable: "FoundedAnimals",
                        principalColumn: "FoundedAnimalId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AnimalImages_DisappearedAnimalEntityDisappearedAnimalId",
                table: "AnimalImages",
                column: "DisappearedAnimalEntityDisappearedAnimalId");

            migrationBuilder.CreateIndex(
                name: "IX_AnimalImages_FoundedAnimalEntityFoundedAnimalId",
                table: "AnimalImages",
                column: "FoundedAnimalEntityFoundedAnimalId");

            migrationBuilder.CreateIndex(
                name: "IX_AnimalSuggestions_DisappearedAnimalId",
                table: "AnimalSuggestions",
                column: "DisappearedAnimalId");

            migrationBuilder.CreateIndex(
                name: "IX_AnimalSuggestions_FoundedAnimalId",
                table: "AnimalSuggestions",
                column: "FoundedAnimalId");

            migrationBuilder.CreateIndex(
                name: "IX_DisappearedAnimals_OwnerId",
                table: "DisappearedAnimals",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_FoundedAnimals_ReporterId",
                table: "FoundedAnimals",
                column: "ReporterId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AnimalImages");

            migrationBuilder.DropTable(
                name: "AnimalSuggestions");

            migrationBuilder.DropTable(
                name: "DisappearedAnimals");

            migrationBuilder.DropTable(
                name: "FoundedAnimals");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "Users");
        }
    }
}
