using Microsoft.EntityFrameworkCore.Migrations;

namespace RankingService.Migrations
{
    public partial class Addgeneralization : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rates_RateTypes_RateTypeId",
                table: "Rates");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Rates",
                table: "Rates");

            migrationBuilder.RenameTable(
                name: "Rates",
                newName: "Rate");

            migrationBuilder.RenameIndex(
                name: "IX_Rates_RateTypeId",
                table: "Rate",
                newName: "IX_Rate_RateTypeId");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Rate",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "PostId",
                table: "Rate",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TransportId",
                table: "Rate",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Rate",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Rate",
                table: "Rate",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Rate_RateTypes_RateTypeId",
                table: "Rate",
                column: "RateTypeId",
                principalTable: "RateTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rate_RateTypes_RateTypeId",
                table: "Rate");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Rate",
                table: "Rate");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Rate");

            migrationBuilder.DropColumn(
                name: "PostId",
                table: "Rate");

            migrationBuilder.DropColumn(
                name: "TransportId",
                table: "Rate");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Rate");

            migrationBuilder.RenameTable(
                name: "Rate",
                newName: "Rates");

            migrationBuilder.RenameIndex(
                name: "IX_Rate_RateTypeId",
                table: "Rates",
                newName: "IX_Rates_RateTypeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Rates",
                table: "Rates",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Rates_RateTypes_RateTypeId",
                table: "Rates",
                column: "RateTypeId",
                principalTable: "RateTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
