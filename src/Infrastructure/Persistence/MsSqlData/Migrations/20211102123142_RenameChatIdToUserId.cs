using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Persistence.MsSqlData.Migrations
{
    public partial class RenameChatIdToUserId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "chatId",
                table: "UserSubscription",
                newName: "userId");

            migrationBuilder.RenameColumn(
                name: "Resource",
                table: "UserSubscription",
                newName: "recource");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "userId",
                table: "UserSubscription",
                newName: "chatId");

            migrationBuilder.RenameColumn(
                name: "recource",
                table: "UserSubscription",
                newName: "Resource");
        }
    }
}
