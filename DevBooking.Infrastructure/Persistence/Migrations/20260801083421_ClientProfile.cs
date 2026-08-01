using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DevBooking.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ClientProfile : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ClientProfileId",
                table: "Bookings",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ClientProfiles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CompanyName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    JobTitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Bio = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Website = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProfileImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientProfiles", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_ClientProfileId",
                table: "Bookings",
                column: "ClientProfileId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_ClientProfiles_ClientProfileId",
                table: "Bookings",
                column: "ClientProfileId",
                principalTable: "ClientProfiles",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_ClientProfiles_ClientProfileId",
                table: "Bookings");

            migrationBuilder.DropTable(
                name: "ClientProfiles");

            migrationBuilder.DropIndex(
                name: "IX_Bookings_ClientProfileId",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "ClientProfileId",
                table: "Bookings");
        }
    }
}
