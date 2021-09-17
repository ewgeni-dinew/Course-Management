using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CourseManagement.Data.Migrations
{
    public partial class Second : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Geo_Lat",
                table: "Users",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Geo_Lng",
                table: "Users",
                type: "decimal(18,2)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Geo_Lat",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Geo_Lng",
                table: "Users");
        }
    }
}
