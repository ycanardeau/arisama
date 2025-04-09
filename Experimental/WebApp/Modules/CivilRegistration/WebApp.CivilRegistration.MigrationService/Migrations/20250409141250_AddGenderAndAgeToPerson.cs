using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApp.CivilRegistration.MigrationService.Migrations
{
    /// <inheritdoc />
    public partial class AddGenderAndAgeToPerson : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Age",
                schema: "WebApp_CivilRegistration",
                table: "Persons",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Gender",
                schema: "WebApp_CivilRegistration",
                table: "Persons",
                type: "varchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Age",
                schema: "WebApp_CivilRegistration",
                table: "Persons");

            migrationBuilder.DropColumn(
                name: "Gender",
                schema: "WebApp_CivilRegistration",
                table: "Persons");
        }
    }
}
