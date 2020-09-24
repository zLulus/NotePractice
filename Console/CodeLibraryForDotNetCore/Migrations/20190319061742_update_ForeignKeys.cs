using Microsoft.EntityFrameworkCore.Migrations;

namespace CodeLibraryForDotNetCore.Migrations
{
    public partial class update_ForeignKeys : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cities_Countries_CountryId",
                schema: "public",
                table: "Cities");

            migrationBuilder.DropForeignKey(
                name: "FK_Roads_Cities_CityId",
                schema: "public",
                table: "Roads");

            migrationBuilder.AlterColumn<long>(
                name: "CityId",
                schema: "public",
                table: "Roads",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.AlterColumn<long>(
                name: "CountryId",
                schema: "public",
                table: "Cities",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.AddForeignKey(
                name: "FK_Cities_Countries_CountryId",
                schema: "public",
                table: "Cities",
                column: "CountryId",
                principalSchema: "public",
                principalTable: "Countries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Roads_Cities_CityId",
                schema: "public",
                table: "Roads",
                column: "CityId",
                principalSchema: "public",
                principalTable: "Cities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
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

            migrationBuilder.AlterColumn<long>(
                name: "CityId",
                schema: "public",
                table: "Roads",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "CountryId",
                schema: "public",
                table: "Cities",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

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
    }
}
