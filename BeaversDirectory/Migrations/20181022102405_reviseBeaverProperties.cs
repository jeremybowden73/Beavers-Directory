using Microsoft.EntityFrameworkCore.Migrations;

namespace BeaversDirectory.Migrations
{
    public partial class reviseBeaverProperties : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ParentEmail",
                table: "Beavers",
                newName: "ParentUserName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ParentUserName",
                table: "Beavers",
                newName: "ParentEmail");
        }
    }
}
