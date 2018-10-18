using Microsoft.EntityFrameworkCore.Migrations;

namespace BeaversDirectory.Migrations
{
    public partial class FeedbackMessageAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Message",
                table: "Feedbacks",
                newName: "FeedbackMessage");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FeedbackMessage",
                table: "Feedbacks",
                newName: "Message");
        }
    }
}
