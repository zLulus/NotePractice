using Microsoft.EntityFrameworkCore.Migrations;

namespace CodeLibraryForDotNetCore.Migrations
{
    public partial class add_ForeignKeys : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "CityId",
                schema: "public",
                table: "Roads",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "CountryId",
                schema: "public",
                table: "Cities",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_Roads_CityId",
                schema: "public",
                table: "Roads",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_Cities_CountryId",
                schema: "public",
                table: "Cities",
                column: "CountryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cities_Countries_CountryId",
                schema: "public",
                table: "Cities",
                column: "CountryId",
                principalSchema: "public",
                principalTable: "Countries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Roads_Cities_CityId",
                schema: "public",
                table: "Roads",
                column: "CityId",
                principalSchema: "public",
                principalTable: "Cities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cities_Countries_CountryId",
                schema: "public",
                table: "Cities");

            migrationBuilder.DropForeignKey(
                name: "FK_Roads_Cities_CityId",
                schema: "public",
                table: "Roads");

            migrationBuilder.DropIndex(
                name: "IX_Roads_CityId",
                schema: "public",
                table: "Roads");

            migrationBuilder.DropIndex(
                name: "IX_Cities_CountryId",
                schema: "public",
                table: "Cities");

            migrationBuilder.DropColumn(
                name: "CityId",
                schema: "public",
                table: "Roads");

            migrationBuilder.DropColumn(
                name: "CountryId",
                schema: "public",
                table: "Cities");
        }
    }
}
