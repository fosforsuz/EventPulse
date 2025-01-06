#nullable disable

using Microsoft.EntityFrameworkCore.Migrations;

namespace EventPulse.Infrastructure.Entities;

/// <inheritdoc />
public partial class Initial : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            "Users",
            table => new
            {
                Id = table.Column<int>("int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                Name = table.Column<string>("nvarchar(100)", maxLength: 100, nullable: false),
                Email = table.Column<string>("nvarchar(100)", maxLength: 100, nullable: false),
                PasswordHash = table.Column<string>("nvarchar(255)", maxLength: 255, nullable: false),
                Role = table.Column<string>("nvarchar(50)", maxLength: 50, nullable: false, defaultValue: "visitor"),
                CreatedAt = table.Column<DateTime>("datetime", nullable: true, defaultValueSql: "(getdate())"),
                UpdateAt = table.Column<DateTime>("datetime", nullable: true),
                IsDeleted = table.Column<bool>("bit", nullable: false)
            },
            constraints: table => { table.PrimaryKey("PK__Users__3214EC075D381621", x => x.Id); });

        migrationBuilder.CreateTable(
            "Events",
            table => new
            {
                Id = table.Column<int>("int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                Title = table.Column<string>("nvarchar(200)", maxLength: 200, nullable: false),
                Description = table.Column<string>("nvarchar(max)", nullable: true),
                Location = table.Column<string>("nvarchar(200)", maxLength: 200, nullable: false),
                EventDate = table.Column<DateTime>("datetime", nullable: false),
                CreatorId = table.Column<int>("int", nullable: false),
                CreatedAt = table.Column<DateTime>("datetime", nullable: true, defaultValueSql: "(getdate())"),
                UpdatedAt = table.Column<DateTime>("datetime", nullable: true),
                IsDeleted = table.Column<bool>("bit", nullable: false),
                IsCompleted = table.Column<bool>("bit", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK__Events__3214EC07ED8162D3", x => x.Id);
                table.ForeignKey(
                    "FK__Events__CreatorI__571DF1D5",
                    x => x.CreatorId,
                    "Users",
                    "Id");
            });

        migrationBuilder.CreateTable(
            "EventParticipants",
            table => new
            {
                Id = table.Column<int>("int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                EventId = table.Column<int>("int", nullable: false),
                UserId = table.Column<int>("int", nullable: false),
                JoinedAt = table.Column<DateTime>("datetime", nullable: true, defaultValueSql: "(getdate())")
            },
            constraints: table =>
            {
                table.PrimaryKey("PK__EventPar__3214EC0743D1905B", x => x.Id);
                table.ForeignKey(
                    "FK__EventPart__Event__5AEE82B9",
                    x => x.EventId,
                    "Events",
                    "Id");
                table.ForeignKey(
                    "FK__EventPart__UserI__5BE2A6F2",
                    x => x.UserId,
                    "Users",
                    "Id");
            });

        migrationBuilder.CreateTable(
            "Notifications",
            table => new
            {
                Id = table.Column<int>("int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                EventId = table.Column<int>("int", nullable: false),
                Message = table.Column<string>("nvarchar(500)", maxLength: 500, nullable: false),
                SentAt = table.Column<DateTime>("datetime", nullable: true, defaultValueSql: "(getdate())"),
                ReadAt = table.Column<DateTime>("datetime", nullable: true),
                IsRead = table.Column<bool>("bit", nullable: true, defaultValue: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK__Notifica__3214EC07A6040B19", x => x.Id);
                table.ForeignKey(
                    "FK__Notificat__Event__60A75C0F",
                    x => x.EventId,
                    "Events",
                    "Id");
            });

        migrationBuilder.CreateIndex(
            "IX_EventParticipants_EventId",
            "EventParticipants",
            "EventId");

        migrationBuilder.CreateIndex(
            "IX_EventParticipants_UserId",
            "EventParticipants",
            "UserId");

        migrationBuilder.CreateIndex(
            "IX_Events_CreatorId",
            "Events",
            "CreatorId");

        migrationBuilder.CreateIndex(
            "IX_Notifications_EventId",
            "Notifications",
            "EventId");

        migrationBuilder.CreateIndex(
            "UQ__Users__A9D105341A680F55",
            "Users",
            "Email",
            unique: true);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            "EventParticipants");

        migrationBuilder.DropTable(
            "Notifications");

        migrationBuilder.DropTable(
            "Events");

        migrationBuilder.DropTable(
            "Users");
    }
}