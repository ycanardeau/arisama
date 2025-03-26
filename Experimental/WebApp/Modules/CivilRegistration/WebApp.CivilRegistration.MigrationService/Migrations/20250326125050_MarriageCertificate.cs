using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApp.CivilRegistration.MigrationService.Migrations
{
    /// <inheritdoc />
    public partial class MarriageCertificate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MarriageCertificates",
                schema: "WebApp_CivilRegistration",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Person1Id = table.Column<int>(type: "int", nullable: false),
                    Person2Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MarriageCertificates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MarriageCertificates_Persons_Person1Id",
                        column: x => x.Person1Id,
                        principalSchema: "WebApp_CivilRegistration",
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MarriageCertificates_Persons_Person2Id",
                        column: x => x.Person2Id,
                        principalSchema: "WebApp_CivilRegistration",
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_MarriageCertificates_Person1Id",
                schema: "WebApp_CivilRegistration",
                table: "MarriageCertificates",
                column: "Person1Id");

            migrationBuilder.CreateIndex(
                name: "IX_MarriageCertificates_Person2Id",
                schema: "WebApp_CivilRegistration",
                table: "MarriageCertificates",
                column: "Person2Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MarriageCertificates",
                schema: "WebApp_CivilRegistration");
        }
    }
}
