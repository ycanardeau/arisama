using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApp.CivilRegistration.MigrationService.Migrations
{
    /// <inheritdoc />
    public partial class AddGuidToCertificates : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "Guid",
                schema: "WebApp_CivilRegistration",
                table: "MarriageCertificates",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Guid",
                schema: "WebApp_CivilRegistration",
                table: "MarriageCertificates");

            migrationBuilder.DropColumn(
                name: "Guid",
                schema: "WebApp_CivilRegistration",
                table: "DivorceCertificates");

            migrationBuilder.DropColumn(
                name: "Guid",
                schema: "WebApp_CivilRegistration",
                table: "DeathCertificates");
        }
    }
}
