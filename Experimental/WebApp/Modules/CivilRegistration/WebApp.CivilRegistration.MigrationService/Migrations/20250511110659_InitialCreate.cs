using System;
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
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Gender = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Age = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Persons", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "DeathCertificates",
                schema: "WebApp_CivilRegistration",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    DeceasedId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    WidowedId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci")
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
                    table.ForeignKey(
                        name: "FK_DeathCertificates_Persons_WidowedId",
                        column: x => x.WidowedId,
                        principalSchema: "WebApp_CivilRegistration",
                        principalTable: "Persons",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "MaritalStateMachines",
                schema: "WebApp_CivilRegistration",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    PersonId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
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
                name: "MarriageCertificates",
                schema: "WebApp_CivilRegistration",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    HusbandId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    WifeId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MarriageCertificates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MarriageCertificates_Persons_HusbandId",
                        column: x => x.HusbandId,
                        principalSchema: "WebApp_CivilRegistration",
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MarriageCertificates_Persons_WifeId",
                        column: x => x.WifeId,
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
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    StateMachineId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Version = table.Column<int>(type: "int", nullable: false),
                    Discriminator = table.Column<string>(type: "varchar(13)", maxLength: 13, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Payload = table.Column<string>(type: "longtext", nullable: true)
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

            migrationBuilder.CreateTable(
                name: "DivorceCertificates",
                schema: "WebApp_CivilRegistration",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    MarriageCertificateId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
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
                name: "IX_DeathCertificates_DeceasedId",
                schema: "WebApp_CivilRegistration",
                table: "DeathCertificates",
                column: "DeceasedId");

            migrationBuilder.CreateIndex(
                name: "IX_DeathCertificates_WidowedId",
                schema: "WebApp_CivilRegistration",
                table: "DeathCertificates",
                column: "WidowedId");

            migrationBuilder.CreateIndex(
                name: "IX_DivorceCertificates_MarriageCertificateId",
                schema: "WebApp_CivilRegistration",
                table: "DivorceCertificates",
                column: "MarriageCertificateId");

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

            migrationBuilder.CreateIndex(
                name: "IX_MarriageCertificates_HusbandId",
                schema: "WebApp_CivilRegistration",
                table: "MarriageCertificates",
                column: "HusbandId");

            migrationBuilder.CreateIndex(
                name: "IX_MarriageCertificates_WifeId",
                schema: "WebApp_CivilRegistration",
                table: "MarriageCertificates",
                column: "WifeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DeathCertificates",
                schema: "WebApp_CivilRegistration");

            migrationBuilder.DropTable(
                name: "DivorceCertificates",
                schema: "WebApp_CivilRegistration");

            migrationBuilder.DropTable(
                name: "MaritalStatuses",
                schema: "WebApp_CivilRegistration");

            migrationBuilder.DropTable(
                name: "MarriageCertificates",
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
