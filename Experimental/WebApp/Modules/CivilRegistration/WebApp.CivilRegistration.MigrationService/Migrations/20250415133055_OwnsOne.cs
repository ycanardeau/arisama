using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApp.CivilRegistration.MigrationService.Migrations
{
    /// <inheritdoc />
    public partial class OwnsOne : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Guid",
                schema: "WebApp_CivilRegistration",
                table: "MarriageCertificates");

            migrationBuilder.DropColumn(
                name: "Payload",
                schema: "WebApp_CivilRegistration",
                table: "MaritalStatuses");

            migrationBuilder.DropColumn(
                name: "Guid",
                schema: "WebApp_CivilRegistration",
                table: "DivorceCertificates");

            migrationBuilder.DropColumn(
                name: "Guid",
                schema: "WebApp_CivilRegistration",
                table: "DeathCertificates");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<Guid>(
                name: "Guid",
                schema: "WebApp_CivilRegistration",
                table: "MarriageCertificates",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<string>(
                name: "Payload",
                schema: "WebApp_CivilRegistration",
                table: "MaritalStatuses",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<Guid>(
                name: "Guid",
                schema: "WebApp_CivilRegistration",
                table: "DivorceCertificates",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<Guid>(
                name: "Guid",
                schema: "WebApp_CivilRegistration",
                table: "DeathCertificates",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci");
        }
    }
}
