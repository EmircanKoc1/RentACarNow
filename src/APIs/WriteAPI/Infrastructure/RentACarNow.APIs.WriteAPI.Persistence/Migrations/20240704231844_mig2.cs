using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RentACarNow.APIs.WriteAPI.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class mig2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AdminClaim_Claim_ClaimsId",
                table: "AdminClaim");

            migrationBuilder.DropForeignKey(
                name: "FK_ClaimCustomer_Claim_ClaimsId",
                table: "ClaimCustomer");

            migrationBuilder.DropForeignKey(
                name: "FK_ClaimEmployee_Claim_ClaimsId",
                table: "ClaimEmployee");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Claim",
                table: "Claim");

            migrationBuilder.RenameTable(
                name: "Claim",
                newName: "Claims");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Claims",
                table: "Claims",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AdminClaim_Claims_ClaimsId",
                table: "AdminClaim",
                column: "ClaimsId",
                principalTable: "Claims",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ClaimCustomer_Claims_ClaimsId",
                table: "ClaimCustomer",
                column: "ClaimsId",
                principalTable: "Claims",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ClaimEmployee_Claims_ClaimsId",
                table: "ClaimEmployee",
                column: "ClaimsId",
                principalTable: "Claims",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AdminClaim_Claims_ClaimsId",
                table: "AdminClaim");

            migrationBuilder.DropForeignKey(
                name: "FK_ClaimCustomer_Claims_ClaimsId",
                table: "ClaimCustomer");

            migrationBuilder.DropForeignKey(
                name: "FK_ClaimEmployee_Claims_ClaimsId",
                table: "ClaimEmployee");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Claims",
                table: "Claims");

            migrationBuilder.RenameTable(
                name: "Claims",
                newName: "Claim");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Claim",
                table: "Claim",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AdminClaim_Claim_ClaimsId",
                table: "AdminClaim",
                column: "ClaimsId",
                principalTable: "Claim",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ClaimCustomer_Claim_ClaimsId",
                table: "ClaimCustomer",
                column: "ClaimsId",
                principalTable: "Claim",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ClaimEmployee_Claim_ClaimsId",
                table: "ClaimEmployee",
                column: "ClaimsId",
                principalTable: "Claim",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
