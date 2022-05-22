using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SportMeetingsApi.Migrations {
    public partial class RunSqlScript : Migration {
        protected override void Up(MigrationBuilder migrationBuilder) {
            migrationBuilder.Sql($"INSERT INTO [dbo].[AspNetRoles] ([Id], [Name], [NormalizedName]) VALUES (1, 'Admin', 'ADMIN')");
            migrationBuilder.Sql($"INSERT INTO [dbo].[AspNetRoles] ([Id], [Name], [NormalizedName]) VALUES (2, 'User', 'USER')");
        }

        protected override void Down(MigrationBuilder migrationBuilder) {

        }
    }
}
