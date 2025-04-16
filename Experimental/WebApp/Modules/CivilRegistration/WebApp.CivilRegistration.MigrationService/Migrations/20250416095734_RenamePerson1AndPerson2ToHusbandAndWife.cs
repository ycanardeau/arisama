using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApp.CivilRegistration.MigrationService.Migrations
{
    /// <inheritdoc />
    public partial class RenamePerson1AndPerson2ToHusbandAndWife : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MarriageCertificates_Persons_Person1Id",
                schema: "WebApp_CivilRegistration",
                table: "MarriageCertificates");

            migrationBuilder.DropForeignKey(
                name: "FK_MarriageCertificates_Persons_Person2Id",
                schema: "WebApp_CivilRegistration",
                table: "MarriageCertificates");

            migrationBuilder.RenameColumn(
                name: "Person2Id",
                schema: "WebApp_CivilRegistration",
                table: "MarriageCertificates",
                newName: "WifeId");

            migrationBuilder.RenameColumn(
                name: "Person1Id",
                schema: "WebApp_CivilRegistration",
                table: "MarriageCertificates",
                newName: "HusbandId");

            migrationBuilder.RenameIndex(
                name: "IX_MarriageCertificates_Person2Id",
                schema: "WebApp_CivilRegistration",
                table: "MarriageCertificates",
                newName: "IX_MarriageCertificates_WifeId");

            migrationBuilder.RenameIndex(
                name: "IX_MarriageCertificates_Person1Id",
                schema: "WebApp_CivilRegistration",
                table: "MarriageCertificates",
                newName: "IX_MarriageCertificates_HusbandId");

            migrationBuilder.AddForeignKey(
                name: "FK_MarriageCertificates_Persons_HusbandId",
                schema: "WebApp_CivilRegistration",
                table: "MarriageCertificates",
                column: "HusbandId",
                principalSchema: "WebApp_CivilRegistration",
                principalTable: "Persons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MarriageCertificates_Persons_WifeId",
                schema: "WebApp_CivilRegistration",
                table: "MarriageCertificates",
                column: "WifeId",
                principalSchema: "WebApp_CivilRegistration",
                principalTable: "Persons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MarriageCertificates_Persons_HusbandId",
                schema: "WebApp_CivilRegistration",
                table: "MarriageCertificates");

            migrationBuilder.DropForeignKey(
                name: "FK_MarriageCertificates_Persons_WifeId",
                schema: "WebApp_CivilRegistration",
                table: "MarriageCertificates");

            migrationBuilder.RenameColumn(
                name: "WifeId",
                schema: "WebApp_CivilRegistration",
                table: "MarriageCertificates",
                newName: "Person2Id");

            migrationBuilder.RenameColumn(
                name: "HusbandId",
                schema: "WebApp_CivilRegistration",
                table: "MarriageCertificates",
                newName: "Person1Id");

            migrationBuilder.RenameIndex(
                name: "IX_MarriageCertificates_WifeId",
                schema: "WebApp_CivilRegistration",
                table: "MarriageCertificates",
                newName: "IX_MarriageCertificates_Person2Id");

            migrationBuilder.RenameIndex(
                name: "IX_MarriageCertificates_HusbandId",
                schema: "WebApp_CivilRegistration",
                table: "MarriageCertificates",
                newName: "IX_MarriageCertificates_Person1Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MarriageCertificates_Persons_Person1Id",
                schema: "WebApp_CivilRegistration",
                table: "MarriageCertificates",
                column: "Person1Id",
                principalSchema: "WebApp_CivilRegistration",
                principalTable: "Persons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MarriageCertificates_Persons_Person2Id",
                schema: "WebApp_CivilRegistration",
                table: "MarriageCertificates",
                column: "Person2Id",
                principalSchema: "WebApp_CivilRegistration",
                principalTable: "Persons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
