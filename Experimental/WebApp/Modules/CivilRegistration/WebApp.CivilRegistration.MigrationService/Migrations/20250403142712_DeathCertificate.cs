using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApp.CivilRegistration.MigrationService.Migrations
{
    /// <inheritdoc />
    public partial class DeathCertificate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DeathCertificates",
                schema: "WebApp_CivilRegistration",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    DeceasedId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeathCertificates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DeathCertificates_Persons_DeceasedId",
                        column: x => x.DeceasedId,
                        principalSchema: "WebApp_CivilRegistration",
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_DeathCertificates_DeceasedId",
                schema: "WebApp_CivilRegistration",
                table: "DeathCertificates",
                column: "DeceasedId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DeathCertificates",
                schema: "WebApp_CivilRegistration");
        }
    }
}
