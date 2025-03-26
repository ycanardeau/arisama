using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApp.CivilRegistration.MigrationService.Migrations
{
    /// <inheritdoc />
    public partial class DivorceCertificate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DivorceCertificates",
                schema: "WebApp_CivilRegistration",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    MarriageCertificateId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DivorceCertificates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DivorceCertificates_MarriageCertificates_MarriageCertificate~",
                        column: x => x.MarriageCertificateId,
                        principalSchema: "WebApp_CivilRegistration",
                        principalTable: "MarriageCertificates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_DivorceCertificates_MarriageCertificateId",
                schema: "WebApp_CivilRegistration",
                table: "DivorceCertificates",
                column: "MarriageCertificateId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DivorceCertificates",
                schema: "WebApp_CivilRegistration");
        }
    }
}
