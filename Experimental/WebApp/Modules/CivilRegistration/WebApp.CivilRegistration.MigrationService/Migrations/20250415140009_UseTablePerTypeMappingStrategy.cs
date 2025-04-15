using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApp.CivilRegistration.MigrationService.Migrations
{
    /// <inheritdoc />
    public partial class UseTablePerTypeMappingStrategy : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MaritalStatuses_DeathCertificates_Payload_DeathInformation_D~",
                schema: "WebApp_CivilRegistration",
                table: "MaritalStatuses");

            migrationBuilder.DropForeignKey(
                name: "FK_MaritalStatuses_DivorceCertificates_Payload_DivorceInformati~",
                schema: "WebApp_CivilRegistration",
                table: "MaritalStatuses");

            migrationBuilder.DropForeignKey(
                name: "FK_MaritalStatuses_MarriageCertificates_Payload_MarriageInforma~",
                schema: "WebApp_CivilRegistration",
                table: "MaritalStatuses");

            migrationBuilder.DropForeignKey(
                name: "FK_MaritalStatuses_Persons_Payload_DivorceInformation_DivorcedF~",
                schema: "WebApp_CivilRegistration",
                table: "MaritalStatuses");

            migrationBuilder.DropForeignKey(
                name: "FK_MaritalStatuses_Persons_Payload_MarriageInformation_MarriedW~",
                schema: "WebApp_CivilRegistration",
                table: "MaritalStatuses");

            migrationBuilder.DropForeignKey(
                name: "FK_MaritalStatuses_Persons_Payload_WidowhoodInformation_Widowed~",
                schema: "WebApp_CivilRegistration",
                table: "MaritalStatuses");

            migrationBuilder.DropIndex(
                name: "IX_MaritalStatuses_Payload_DeathInformation_DeathCertificateId",
                schema: "WebApp_CivilRegistration",
                table: "MaritalStatuses");

            migrationBuilder.DropIndex(
                name: "IX_MaritalStatuses_Payload_DivorceInformation_DivorceCertificat~",
                schema: "WebApp_CivilRegistration",
                table: "MaritalStatuses");

            migrationBuilder.DropIndex(
                name: "IX_MaritalStatuses_Payload_DivorceInformation_DivorcedFromId",
                schema: "WebApp_CivilRegistration",
                table: "MaritalStatuses");

            migrationBuilder.DropIndex(
                name: "IX_MaritalStatuses_Payload_MarriageInformation_MarriageCertific~",
                schema: "WebApp_CivilRegistration",
                table: "MaritalStatuses");

            migrationBuilder.DropIndex(
                name: "IX_MaritalStatuses_Payload_MarriageInformation_MarriedWithId",
                schema: "WebApp_CivilRegistration",
                table: "MaritalStatuses");

            migrationBuilder.DropIndex(
                name: "IX_MaritalStatuses_Payload_WidowhoodInformation_WidowedFromId",
                schema: "WebApp_CivilRegistration",
                table: "MaritalStatuses");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                schema: "WebApp_CivilRegistration",
                table: "MaritalStatuses");

            migrationBuilder.DropColumn(
                name: "Payload_DeathInformation_DeathCertificateId",
                schema: "WebApp_CivilRegistration",
                table: "MaritalStatuses");

            migrationBuilder.DropColumn(
                name: "Payload_DeathInformation_DeceasedAtAge",
                schema: "WebApp_CivilRegistration",
                table: "MaritalStatuses");

            migrationBuilder.DropColumn(
                name: "Payload_DivorceInformation_DivorceCertificateId",
                schema: "WebApp_CivilRegistration",
                table: "MaritalStatuses");

            migrationBuilder.DropColumn(
                name: "Payload_DivorceInformation_DivorcedAtAge",
                schema: "WebApp_CivilRegistration",
                table: "MaritalStatuses");

            migrationBuilder.DropColumn(
                name: "Payload_DivorceInformation_DivorcedFromId",
                schema: "WebApp_CivilRegistration",
                table: "MaritalStatuses");

            migrationBuilder.DropColumn(
                name: "Payload_MarriageInformation_MarriageCertificateId",
                schema: "WebApp_CivilRegistration",
                table: "MaritalStatuses");

            migrationBuilder.DropColumn(
                name: "Payload_MarriageInformation_MarriedAtAge",
                schema: "WebApp_CivilRegistration",
                table: "MaritalStatuses");

            migrationBuilder.DropColumn(
                name: "Payload_MarriageInformation_MarriedWithId",
                schema: "WebApp_CivilRegistration",
                table: "MaritalStatuses");

            migrationBuilder.DropColumn(
                name: "Payload_WidowhoodInformation_WidowedAtAge",
                schema: "WebApp_CivilRegistration",
                table: "MaritalStatuses");

            migrationBuilder.DropColumn(
                name: "Payload_WidowhoodInformation_WidowedFromId",
                schema: "WebApp_CivilRegistration",
                table: "MaritalStatuses");

            migrationBuilder.CreateTable(
                name: "DeceasedStates",
                schema: "WebApp_CivilRegistration",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Payload_DeathInformation_DeathCertificateId = table.Column<int>(type: "int", nullable: false),
                    Payload_DeathInformation_DeceasedAtAge = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeceasedStates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DeceasedStates_DeathCertificates_Payload_DeathInformation_De~",
                        column: x => x.Payload_DeathInformation_DeathCertificateId,
                        principalSchema: "WebApp_CivilRegistration",
                        principalTable: "DeathCertificates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DeceasedStates_MaritalStatuses_Id",
                        column: x => x.Id,
                        principalSchema: "WebApp_CivilRegistration",
                        principalTable: "MaritalStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "DivorcedStates",
                schema: "WebApp_CivilRegistration",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Payload_MarriageInformation_MarriageCertificateId = table.Column<int>(type: "int", nullable: false),
                    Payload_MarriageInformation_MarriedAtAge = table.Column<int>(type: "int", nullable: false),
                    Payload_MarriageInformation_MarriedWithId = table.Column<int>(type: "int", nullable: false),
                    Payload_DivorceInformation_DivorceCertificateId = table.Column<int>(type: "int", nullable: false),
                    Payload_DivorceInformation_DivorcedAtAge = table.Column<int>(type: "int", nullable: false),
                    Payload_DivorceInformation_DivorcedFromId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DivorcedStates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DivorcedStates_DivorceCertificates_Payload_DivorceInformatio~",
                        column: x => x.Payload_DivorceInformation_DivorceCertificateId,
                        principalSchema: "WebApp_CivilRegistration",
                        principalTable: "DivorceCertificates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DivorcedStates_MaritalStatuses_Id",
                        column: x => x.Id,
                        principalSchema: "WebApp_CivilRegistration",
                        principalTable: "MaritalStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DivorcedStates_MarriageCertificates_Payload_MarriageInformat~",
                        column: x => x.Payload_MarriageInformation_MarriageCertificateId,
                        principalSchema: "WebApp_CivilRegistration",
                        principalTable: "MarriageCertificates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DivorcedStates_Persons_Payload_DivorceInformation_DivorcedFr~",
                        column: x => x.Payload_DivorceInformation_DivorcedFromId,
                        principalSchema: "WebApp_CivilRegistration",
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DivorcedStates_Persons_Payload_MarriageInformation_MarriedWi~",
                        column: x => x.Payload_MarriageInformation_MarriedWithId,
                        principalSchema: "WebApp_CivilRegistration",
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "MarriedStates",
                schema: "WebApp_CivilRegistration",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Payload_MarriageInformation_MarriageCertificateId = table.Column<int>(type: "int", nullable: false),
                    Payload_MarriageInformation_MarriedAtAge = table.Column<int>(type: "int", nullable: false),
                    Payload_MarriageInformation_MarriedWithId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MarriedStates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MarriedStates_MaritalStatuses_Id",
                        column: x => x.Id,
                        principalSchema: "WebApp_CivilRegistration",
                        principalTable: "MaritalStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MarriedStates_MarriageCertificates_Payload_MarriageInformati~",
                        column: x => x.Payload_MarriageInformation_MarriageCertificateId,
                        principalSchema: "WebApp_CivilRegistration",
                        principalTable: "MarriageCertificates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MarriedStates_Persons_Payload_MarriageInformation_MarriedWit~",
                        column: x => x.Payload_MarriageInformation_MarriedWithId,
                        principalSchema: "WebApp_CivilRegistration",
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "SingleStates",
                schema: "WebApp_CivilRegistration",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SingleStates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SingleStates_MaritalStatuses_Id",
                        column: x => x.Id,
                        principalSchema: "WebApp_CivilRegistration",
                        principalTable: "MaritalStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "WidowedStates",
                schema: "WebApp_CivilRegistration",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Payload_MarriageInformation_MarriageCertificateId = table.Column<int>(type: "int", nullable: false),
                    Payload_MarriageInformation_MarriedAtAge = table.Column<int>(type: "int", nullable: false),
                    Payload_MarriageInformation_MarriedWithId = table.Column<int>(type: "int", nullable: false),
                    Payload_WidowhoodInformation_WidowedAtAge = table.Column<int>(type: "int", nullable: false),
                    Payload_WidowhoodInformation_WidowedFromId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WidowedStates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WidowedStates_MaritalStatuses_Id",
                        column: x => x.Id,
                        principalSchema: "WebApp_CivilRegistration",
                        principalTable: "MaritalStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WidowedStates_MarriageCertificates_Payload_MarriageInformati~",
                        column: x => x.Payload_MarriageInformation_MarriageCertificateId,
                        principalSchema: "WebApp_CivilRegistration",
                        principalTable: "MarriageCertificates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WidowedStates_Persons_Payload_MarriageInformation_MarriedWit~",
                        column: x => x.Payload_MarriageInformation_MarriedWithId,
                        principalSchema: "WebApp_CivilRegistration",
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WidowedStates_Persons_Payload_WidowhoodInformation_WidowedFr~",
                        column: x => x.Payload_WidowhoodInformation_WidowedFromId,
                        principalSchema: "WebApp_CivilRegistration",
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_DeceasedStates_Payload_DeathInformation_DeathCertificateId",
                schema: "WebApp_CivilRegistration",
                table: "DeceasedStates",
                column: "Payload_DeathInformation_DeathCertificateId");

            migrationBuilder.CreateIndex(
                name: "IX_DivorcedStates_Payload_DivorceInformation_DivorceCertificate~",
                schema: "WebApp_CivilRegistration",
                table: "DivorcedStates",
                column: "Payload_DivorceInformation_DivorceCertificateId");

            migrationBuilder.CreateIndex(
                name: "IX_DivorcedStates_Payload_DivorceInformation_DivorcedFromId",
                schema: "WebApp_CivilRegistration",
                table: "DivorcedStates",
                column: "Payload_DivorceInformation_DivorcedFromId");

            migrationBuilder.CreateIndex(
                name: "IX_DivorcedStates_Payload_MarriageInformation_MarriageCertifica~",
                schema: "WebApp_CivilRegistration",
                table: "DivorcedStates",
                column: "Payload_MarriageInformation_MarriageCertificateId");

            migrationBuilder.CreateIndex(
                name: "IX_DivorcedStates_Payload_MarriageInformation_MarriedWithId",
                schema: "WebApp_CivilRegistration",
                table: "DivorcedStates",
                column: "Payload_MarriageInformation_MarriedWithId");

            migrationBuilder.CreateIndex(
                name: "IX_MarriedStates_Payload_MarriageInformation_MarriageCertificat~",
                schema: "WebApp_CivilRegistration",
                table: "MarriedStates",
                column: "Payload_MarriageInformation_MarriageCertificateId");

            migrationBuilder.CreateIndex(
                name: "IX_MarriedStates_Payload_MarriageInformation_MarriedWithId",
                schema: "WebApp_CivilRegistration",
                table: "MarriedStates",
                column: "Payload_MarriageInformation_MarriedWithId");

            migrationBuilder.CreateIndex(
                name: "IX_WidowedStates_Payload_MarriageInformation_MarriageCertificat~",
                schema: "WebApp_CivilRegistration",
                table: "WidowedStates",
                column: "Payload_MarriageInformation_MarriageCertificateId");

            migrationBuilder.CreateIndex(
                name: "IX_WidowedStates_Payload_MarriageInformation_MarriedWithId",
                schema: "WebApp_CivilRegistration",
                table: "WidowedStates",
                column: "Payload_MarriageInformation_MarriedWithId");

            migrationBuilder.CreateIndex(
                name: "IX_WidowedStates_Payload_WidowhoodInformation_WidowedFromId",
                schema: "WebApp_CivilRegistration",
                table: "WidowedStates",
                column: "Payload_WidowhoodInformation_WidowedFromId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DeceasedStates",
                schema: "WebApp_CivilRegistration");

            migrationBuilder.DropTable(
                name: "DivorcedStates",
                schema: "WebApp_CivilRegistration");

            migrationBuilder.DropTable(
                name: "MarriedStates",
                schema: "WebApp_CivilRegistration");

            migrationBuilder.DropTable(
                name: "SingleStates",
                schema: "WebApp_CivilRegistration");

            migrationBuilder.DropTable(
                name: "WidowedStates",
                schema: "WebApp_CivilRegistration");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                schema: "WebApp_CivilRegistration",
                table: "MaritalStatuses",
                type: "varchar(13)",
                maxLength: 13,
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "Payload_DeathInformation_DeathCertificateId",
                schema: "WebApp_CivilRegistration",
                table: "MaritalStatuses",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Payload_DeathInformation_DeceasedAtAge",
                schema: "WebApp_CivilRegistration",
                table: "MaritalStatuses",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Payload_DivorceInformation_DivorceCertificateId",
                schema: "WebApp_CivilRegistration",
                table: "MaritalStatuses",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Payload_DivorceInformation_DivorcedAtAge",
                schema: "WebApp_CivilRegistration",
                table: "MaritalStatuses",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Payload_DivorceInformation_DivorcedFromId",
                schema: "WebApp_CivilRegistration",
                table: "MaritalStatuses",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Payload_MarriageInformation_MarriageCertificateId",
                schema: "WebApp_CivilRegistration",
                table: "MaritalStatuses",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Payload_MarriageInformation_MarriedAtAge",
                schema: "WebApp_CivilRegistration",
                table: "MaritalStatuses",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Payload_MarriageInformation_MarriedWithId",
                schema: "WebApp_CivilRegistration",
                table: "MaritalStatuses",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Payload_WidowhoodInformation_WidowedAtAge",
                schema: "WebApp_CivilRegistration",
                table: "MaritalStatuses",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Payload_WidowhoodInformation_WidowedFromId",
                schema: "WebApp_CivilRegistration",
                table: "MaritalStatuses",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MaritalStatuses_Payload_DeathInformation_DeathCertificateId",
                schema: "WebApp_CivilRegistration",
                table: "MaritalStatuses",
                column: "Payload_DeathInformation_DeathCertificateId");

            migrationBuilder.CreateIndex(
                name: "IX_MaritalStatuses_Payload_DivorceInformation_DivorceCertificat~",
                schema: "WebApp_CivilRegistration",
                table: "MaritalStatuses",
                column: "Payload_DivorceInformation_DivorceCertificateId");

            migrationBuilder.CreateIndex(
                name: "IX_MaritalStatuses_Payload_DivorceInformation_DivorcedFromId",
                schema: "WebApp_CivilRegistration",
                table: "MaritalStatuses",
                column: "Payload_DivorceInformation_DivorcedFromId");

            migrationBuilder.CreateIndex(
                name: "IX_MaritalStatuses_Payload_MarriageInformation_MarriageCertific~",
                schema: "WebApp_CivilRegistration",
                table: "MaritalStatuses",
                column: "Payload_MarriageInformation_MarriageCertificateId");

            migrationBuilder.CreateIndex(
                name: "IX_MaritalStatuses_Payload_MarriageInformation_MarriedWithId",
                schema: "WebApp_CivilRegistration",
                table: "MaritalStatuses",
                column: "Payload_MarriageInformation_MarriedWithId");

            migrationBuilder.CreateIndex(
                name: "IX_MaritalStatuses_Payload_WidowhoodInformation_WidowedFromId",
                schema: "WebApp_CivilRegistration",
                table: "MaritalStatuses",
                column: "Payload_WidowhoodInformation_WidowedFromId");

            migrationBuilder.AddForeignKey(
                name: "FK_MaritalStatuses_DeathCertificates_Payload_DeathInformation_D~",
                schema: "WebApp_CivilRegistration",
                table: "MaritalStatuses",
                column: "Payload_DeathInformation_DeathCertificateId",
                principalSchema: "WebApp_CivilRegistration",
                principalTable: "DeathCertificates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MaritalStatuses_DivorceCertificates_Payload_DivorceInformati~",
                schema: "WebApp_CivilRegistration",
                table: "MaritalStatuses",
                column: "Payload_DivorceInformation_DivorceCertificateId",
                principalSchema: "WebApp_CivilRegistration",
                principalTable: "DivorceCertificates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MaritalStatuses_MarriageCertificates_Payload_MarriageInforma~",
                schema: "WebApp_CivilRegistration",
                table: "MaritalStatuses",
                column: "Payload_MarriageInformation_MarriageCertificateId",
                principalSchema: "WebApp_CivilRegistration",
                principalTable: "MarriageCertificates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MaritalStatuses_Persons_Payload_DivorceInformation_DivorcedF~",
                schema: "WebApp_CivilRegistration",
                table: "MaritalStatuses",
                column: "Payload_DivorceInformation_DivorcedFromId",
                principalSchema: "WebApp_CivilRegistration",
                principalTable: "Persons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MaritalStatuses_Persons_Payload_MarriageInformation_MarriedW~",
                schema: "WebApp_CivilRegistration",
                table: "MaritalStatuses",
                column: "Payload_MarriageInformation_MarriedWithId",
                principalSchema: "WebApp_CivilRegistration",
                principalTable: "Persons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MaritalStatuses_Persons_Payload_WidowhoodInformation_Widowed~",
                schema: "WebApp_CivilRegistration",
                table: "MaritalStatuses",
                column: "Payload_WidowhoodInformation_WidowedFromId",
                principalSchema: "WebApp_CivilRegistration",
                principalTable: "Persons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
