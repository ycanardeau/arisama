using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApp.CivilRegistration.MigrationService.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "WebApp_CivilRegistration");

            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Persons",
                schema: "WebApp_CivilRegistration",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Persons", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "MaritalStateMachines",
                schema: "WebApp_CivilRegistration",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    PersonId = table.Column<int>(type: "int", nullable: false),
                    Version = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaritalStateMachines", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MaritalStateMachines_Persons_PersonId",
                        column: x => x.PersonId,
                        principalSchema: "WebApp_CivilRegistration",
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "MaritalStatuses",
                schema: "WebApp_CivilRegistration",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    StateMachineId = table.Column<int>(type: "int", nullable: false),
                    Version = table.Column<int>(type: "int", nullable: false),
                    Discriminator = table.Column<string>(type: "varchar(13)", maxLength: 13, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaritalStatuses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MaritalStatuses_MaritalStateMachines_StateMachineId",
                        column: x => x.StateMachineId,
                        principalSchema: "WebApp_CivilRegistration",
                        principalTable: "MaritalStateMachines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_MaritalStateMachines_PersonId",
                schema: "WebApp_CivilRegistration",
                table: "MaritalStateMachines",
                column: "PersonId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MaritalStatuses_StateMachineId_Version",
                schema: "WebApp_CivilRegistration",
                table: "MaritalStatuses",
                columns: new[] { "StateMachineId", "Version" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MaritalStatuses",
                schema: "WebApp_CivilRegistration");

            migrationBuilder.DropTable(
                name: "MaritalStateMachines",
                schema: "WebApp_CivilRegistration");

            migrationBuilder.DropTable(
                name: "Persons",
                schema: "WebApp_CivilRegistration");
        }
    }
}
