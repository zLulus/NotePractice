using GeoAPI.Geometries;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CoreWebsite.EntityFramework.Migrations
{
    public partial class delete_Road_Line : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Line",
                table: "Roads");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<ILineString>(
                name: "Line",
                table: "Roads",
                nullable: true);
        }
    }
}
