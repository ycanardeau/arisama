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
			migrationBuilder.EnsureSchema(name: "WebApp_CivilRegistration");

			migrationBuilder.CreateTable(
				name: "People",
				schema: "WebApp_CivilRegistration",
				columns: table => new
				{
					Id = table.Column<Guid>(type: "uuid", nullable: false),
					Gender = table.Column<string>(
						type: "character varying(255)",
						maxLength: 255,
						nullable: false
					),
					Age = table.Column<int>(type: "integer", nullable: false),
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_People", x => x.Id);
				}
			);

			migrationBuilder.CreateTable(
				name: "DeathCertificates",
				schema: "WebApp_CivilRegistration",
				columns: table => new
				{
					Id = table.Column<Guid>(type: "uuid", nullable: false),
					DeceasedId = table.Column<Guid>(type: "uuid", nullable: false),
					WidowedId = table.Column<Guid>(type: "uuid", nullable: true),
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_DeathCertificates", x => x.Id);
					table.ForeignKey(
						name: "FK_DeathCertificates_People_DeceasedId",
						column: x => x.DeceasedId,
						principalSchema: "WebApp_CivilRegistration",
						principalTable: "People",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade
					);
					table.ForeignKey(
						name: "FK_DeathCertificates_People_WidowedId",
						column: x => x.WidowedId,
						principalSchema: "WebApp_CivilRegistration",
						principalTable: "People",
						principalColumn: "Id"
					);
				}
			);

			migrationBuilder.CreateTable(
				name: "MaritalStateMachines",
				schema: "WebApp_CivilRegistration",
				columns: table => new
				{
					Id = table.Column<Guid>(type: "uuid", nullable: false),
					PersonId = table.Column<Guid>(type: "uuid", nullable: false),
					Version = table.Column<int>(type: "integer", nullable: false),
					States = table.Column<string>(type: "text", nullable: false),
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_MaritalStateMachines", x => x.Id);
					table.ForeignKey(
						name: "FK_MaritalStateMachines_People_PersonId",
						column: x => x.PersonId,
						principalSchema: "WebApp_CivilRegistration",
						principalTable: "People",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade
					);
				}
			);

			migrationBuilder.CreateTable(
				name: "MarriageCertificates",
				schema: "WebApp_CivilRegistration",
				columns: table => new
				{
					Id = table.Column<Guid>(type: "uuid", nullable: false),
					HusbandId = table.Column<Guid>(type: "uuid", nullable: false),
					WifeId = table.Column<Guid>(type: "uuid", nullable: false),
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_MarriageCertificates", x => x.Id);
					table.ForeignKey(
						name: "FK_MarriageCertificates_People_HusbandId",
						column: x => x.HusbandId,
						principalSchema: "WebApp_CivilRegistration",
						principalTable: "People",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade
					);
					table.ForeignKey(
						name: "FK_MarriageCertificates_People_WifeId",
						column: x => x.WifeId,
						principalSchema: "WebApp_CivilRegistration",
						principalTable: "People",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade
					);
				}
			);

			migrationBuilder.CreateTable(
				name: "DivorceCertificates",
				schema: "WebApp_CivilRegistration",
				columns: table => new
				{
					Id = table.Column<Guid>(type: "uuid", nullable: false),
					MarriageCertificateId = table.Column<Guid>(type: "uuid", nullable: false),
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_DivorceCertificates", x => x.Id);
					table.ForeignKey(
						name: "FK_DivorceCertificates_MarriageCertificates_MarriageCertificat~",
						column: x => x.MarriageCertificateId,
						principalSchema: "WebApp_CivilRegistration",
						principalTable: "MarriageCertificates",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade
					);
				}
			);

			migrationBuilder.CreateIndex(
				name: "IX_DeathCertificates_DeceasedId",
				schema: "WebApp_CivilRegistration",
				table: "DeathCertificates",
				column: "DeceasedId"
			);

			migrationBuilder.CreateIndex(
				name: "IX_DeathCertificates_WidowedId",
				schema: "WebApp_CivilRegistration",
				table: "DeathCertificates",
				column: "WidowedId"
			);

			migrationBuilder.CreateIndex(
				name: "IX_DivorceCertificates_MarriageCertificateId",
				schema: "WebApp_CivilRegistration",
				table: "DivorceCertificates",
				column: "MarriageCertificateId"
			);

			migrationBuilder.CreateIndex(
				name: "IX_MaritalStateMachines_PersonId",
				schema: "WebApp_CivilRegistration",
				table: "MaritalStateMachines",
				column: "PersonId",
				unique: true
			);

			migrationBuilder.CreateIndex(
				name: "IX_MarriageCertificates_HusbandId",
				schema: "WebApp_CivilRegistration",
				table: "MarriageCertificates",
				column: "HusbandId"
			);

			migrationBuilder.CreateIndex(
				name: "IX_MarriageCertificates_WifeId",
				schema: "WebApp_CivilRegistration",
				table: "MarriageCertificates",
				column: "WifeId"
			);
		}

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropTable(
				name: "DeathCertificates",
				schema: "WebApp_CivilRegistration"
			);

			migrationBuilder.DropTable(
				name: "DivorceCertificates",
				schema: "WebApp_CivilRegistration"
			);

			migrationBuilder.DropTable(
				name: "MaritalStateMachines",
				schema: "WebApp_CivilRegistration"
			);

			migrationBuilder.DropTable(
				name: "MarriageCertificates",
				schema: "WebApp_CivilRegistration"
			);

			migrationBuilder.DropTable(name: "People", schema: "WebApp_CivilRegistration");
		}
	}
}
