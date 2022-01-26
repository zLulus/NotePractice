using Microsoft.EntityFrameworkCore.Migrations;

namespace SchoolManagement.EntityFrameworkCore.Migrations
{
    public partial class add_student : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "SchoolManagement");

            migrationBuilder.CreateTable(
                name: "Student",
                schema: "SchoolManagement",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Major = table.Column<int>(type: "int", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Student", x => x.Id);
                });

            migrationBuilder.InsertData(
                schema: "SchoolManagement",
                table: "Student",
                columns: new[] { "Id", "Email", "Major", "Name" },
                values: new object[] { 2, "zhangsan@qq.com", 3, "张三" });

            migrationBuilder.InsertData(
                schema: "SchoolManagement",
                table: "Student",
                columns: new[] { "Id", "Email", "Major", "Name" },
                values: new object[] { 3, "lisi@360.com", 0, "李四" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Student",
                schema: "SchoolManagement");
        }
    }
}
