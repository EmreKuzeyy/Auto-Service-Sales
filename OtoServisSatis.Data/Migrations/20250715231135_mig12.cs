using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OtoServisSatis.Data.Migrations
{
    /// <inheritdoc />
    public partial class mig12 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateAdded", "UserGuid" },
                values: new object[] { new DateTime(2025, 7, 16, 2, 11, 35, 201, DateTimeKind.Local).AddTicks(8505), new Guid("e4af6582-eb82-454c-acc0-b3621c608772") });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateAdded", "UserGuid" },
                values: new object[] { new DateTime(2025, 7, 14, 20, 23, 54, 635, DateTimeKind.Local).AddTicks(9863), new Guid("f4e5f9a4-dd3d-45fe-8e23-d3dbd4461818") });
        }
    }
}
