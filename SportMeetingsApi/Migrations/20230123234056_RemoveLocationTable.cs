using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SportMeetingsApi.Migrations
{
    public partial class RemoveLocationTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SportEvents_AspNetUsers_UserId",
                table: "SportEvents");

            migrationBuilder.DropForeignKey(
                name: "FK_SportEvents_Locations_LocationId",
                table: "SportEvents");

            migrationBuilder.DropTable(
                name: "Locations");

            migrationBuilder.DropIndex(
                name: "IX_SportEvents_LocationId",
                table: "SportEvents");

            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "SportEvents");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "SportEvents",
                newName: "OwnerId");

            migrationBuilder.RenameColumn(
                name: "LocationId",
                table: "SportEvents",
                newName: "DurationInHours");

            migrationBuilder.RenameIndex(
                name: "IX_SportEvents_UserId",
                table: "SportEvents",
                newName: "IX_SportEvents_OwnerId");

            migrationBuilder.AddColumn<string>(
                name: "Location",
                table: "SportEvents",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_SportEvents_AspNetUsers_OwnerId",
                table: "SportEvents",
                column: "OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SportEvents_AspNetUsers_OwnerId",
                table: "SportEvents");

            migrationBuilder.DropColumn(
                name: "Location",
                table: "SportEvents");

            migrationBuilder.RenameColumn(
                name: "OwnerId",
                table: "SportEvents",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "DurationInHours",
                table: "SportEvents",
                newName: "LocationId");

            migrationBuilder.RenameIndex(
                name: "IX_SportEvents_OwnerId",
                table: "SportEvents",
                newName: "IX_SportEvents_UserId");

            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                table: "SportEvents",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "Locations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Street = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ZipCode = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locations", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SportEvents_LocationId",
                table: "SportEvents",
                column: "LocationId");

            migrationBuilder.AddForeignKey(
                name: "FK_SportEvents_AspNetUsers_UserId",
                table: "SportEvents",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SportEvents_Locations_LocationId",
                table: "SportEvents",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "Id");
        }
    }
}
