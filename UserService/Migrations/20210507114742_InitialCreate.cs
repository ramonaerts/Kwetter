using Microsoft.EntityFrameworkCore.Migrations;

namespace UserService.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Email = table.Column<string>(nullable: true),
                    Username = table.Column<string>(nullable: true),
                    Nickname = table.Column<string>(nullable: true),
                    Location = table.Column<string>(nullable: true),
                    Image = table.Column<string>(nullable: true),
                    FollowersCount = table.Column<int>(nullable: false),
                    FollowingCount = table.Column<int>(nullable: false),
                    Verified = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
