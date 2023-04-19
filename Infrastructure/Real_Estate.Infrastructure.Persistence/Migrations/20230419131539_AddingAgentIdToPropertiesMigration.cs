using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Real_Estate.Infrastructure.Persistence.Migrations
{
    public partial class AddingAgentIdToPropertiesMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AgentId",
                table: "Properties",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AgentId",
                table: "Properties");
        }
    }
}
