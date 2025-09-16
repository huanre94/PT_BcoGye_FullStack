using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BE.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedUsersDataFixed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "user_name",
                table: "users",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "role",
                table: "users",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "id", "actualizado", "creado", "es_activo", "password", "role", "user_name" },
                values: new object[,]
                {
                    { new Guid("11111111-1111-1111-1111-111111111111"), null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, new byte[] { 36, 11, 229, 24, 250, 189, 39, 36, 221, 182, 240, 78, 235, 29, 165, 150, 116, 72, 215, 232, 49, 192, 140, 143, 168, 34, 128, 159, 116, 199, 32, 169 }, 0, "admin@test.com" },
                    { new Guid("22222222-2222-2222-2222-222222222222"), null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, new byte[] { 230, 6, 227, 139, 13, 140, 25, 178, 76, 240, 238, 56, 8, 24, 49, 98, 234, 124, 214, 63, 247, 145, 45, 187, 34, 181, 232, 3, 40, 107, 68, 70 }, 1, "user@test.com" }
                });

            migrationBuilder.CreateIndex(
                name: "ix_users_user_name",
                table: "users",
                column: "user_name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_users_user_name",
                table: "users");

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"));

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"));

            migrationBuilder.DropColumn(
                name: "role",
                table: "users");

            migrationBuilder.AlterColumn<string>(
                name: "user_name",
                table: "users",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);
        }
    }
}
