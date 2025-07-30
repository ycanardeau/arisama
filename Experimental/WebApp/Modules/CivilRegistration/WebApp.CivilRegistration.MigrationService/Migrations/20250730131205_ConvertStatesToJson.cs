using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApp.CivilRegistration.MigrationService.Migrations
{
    /// <inheritdoc />
    public partial class ConvertStatesToJson : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MaritalStatuses",
                schema: "WebApp_CivilRegistration");

            migrationBuilder.AddColumn<string>(
                name: "States",
                schema: "WebApp_CivilRegistration",
                table: "MaritalStateMachines",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "States",
                schema: "WebApp_CivilRegistration",
                table: "MaritalStateMachines");

            migrationBuilder.CreateTable(
                name: "MaritalStatuses",
                schema: "WebApp_CivilRegistration",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    StateMachineId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Discriminator = table.Column<string>(type: "varchar(13)", maxLength: 13, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Version = table.Column<int>(type: "int", nullable: false),
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

            migrationBuilder.CreateIndex(
                name: "IX_MaritalStatuses_StateMachineId_Version",
                schema: "WebApp_CivilRegistration",
                table: "MaritalStatuses",
                columns: new[] { "StateMachineId", "Version" },
                unique: true);
        }
    }
}
