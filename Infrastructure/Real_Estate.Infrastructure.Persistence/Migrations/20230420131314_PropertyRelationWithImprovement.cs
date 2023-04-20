using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Real_Estate.Infrastructure.Persistence.Migrations
{
    public partial class PropertyRelationWithImprovement : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Properties_Improvements_TypeOfPropertyId",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "ImprovementsId",
                table: "Properties");

            migrationBuilder.AlterColumn<string>(
                name: "ImagePathOne",
                table: "Properties",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateTable(
                name: "PropertiesImprovements",
                columns: table => new
                {
                    PropertyId = table.Column<int>(type: "int", nullable: false),
                    ImprovementId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PropertiesImprovements", x => new { x.PropertyId, x.ImprovementId });
                    table.ForeignKey(
                        name: "FK_PropertiesImprovements_Improvements_ImprovementId",
                        column: x => x.ImprovementId,
                        principalTable: "Improvements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PropertiesImprovements_Properties_PropertyId",
                        column: x => x.PropertyId,
                        principalTable: "Properties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PropertiesImprovements_ImprovementId",
                table: "PropertiesImprovements",
                column: "ImprovementId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PropertiesImprovements");

            migrationBuilder.AlterColumn<string>(
                name: "ImagePathOne",
                table: "Properties",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ImprovementsId",
                table: "Properties",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Properties_Improvements_TypeOfPropertyId",
                table: "Properties",
                column: "TypeOfPropertyId",
                principalTable: "Improvements",
                principalColumn: "Id");
        }
    }
}
