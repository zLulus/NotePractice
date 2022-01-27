using Microsoft.EntityFrameworkCore.Migrations;

namespace SchoolManagement.EntityFrameworkCore.Migrations
{
    public partial class add_course_score : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Course",
                schema: "SchoolManagement",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Course", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Scroe",
                schema: "SchoolManagement",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CourseId = table.Column<int>(type: "int", nullable: false),
                    StudentId = table.Column<int>(type: "int", nullable: false),
                    ScroeNumber = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Scroe", x => x.Id);
                });

            migrationBuilder.InsertData(
                schema: "SchoolManagement",
                table: "Course",
                columns: new[] { "Id", "Title" },
                values: new object[] { 1, "数学" });

            migrationBuilder.InsertData(
                schema: "SchoolManagement",
                table: "Scroe",
                columns: new[] { "Id", "CourseId", "ScroeNumber", "StudentId" },
                values: new object[] { 1, 1, 99, 2 });

            migrationBuilder.InsertData(
                schema: "SchoolManagement",
                table: "Scroe",
                columns: new[] { "Id", "CourseId", "ScroeNumber", "StudentId" },
                values: new object[] { 2, 1, 90, 3 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Course",
                schema: "SchoolManagement");

            migrationBuilder.DropTable(
                name: "Scroe",
                schema: "SchoolManagement");
        }
    }
}
