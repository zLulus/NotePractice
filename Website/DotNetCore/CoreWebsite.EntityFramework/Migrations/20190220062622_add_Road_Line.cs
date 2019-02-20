using GeoAPI.Geometries;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CoreWebsite.EntityFramework.Migrations
{
    public partial class add_Road_Line : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<ILineString>(
                name: "Line",
                table: "Roads",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Line",
                table: "Roads");
        }
    }
}
