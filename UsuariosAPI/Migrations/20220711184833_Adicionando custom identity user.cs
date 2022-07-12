using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace UsuariosAPI.Migrations
{
    public partial class Adicionandocustomidentityuser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DataNascimento",
                table: "AspNetUsers",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 99998,
                column: "ConcurrencyStamp",
                value: "b053e5d3-2dab-4914-b88d-ed7bcd857418");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 99999,
                column: "ConcurrencyStamp",
                value: "099d6082-5f86-41d6-9124-58468578a377");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 99999,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e9f5a9e4-11ef-43ee-a96e-3688812bd1eb", "AQAAAAEAACcQAAAAEGiRq5i3bXXwUjnaNxK+XJsPrnJXI2PjnhG9yi/VP6tB1TcA6C+5/KTZ0GngOp/onA==", "e7f470b1-9e9f-423d-aa53-776f49e7752b" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DataNascimento",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 99998,
                column: "ConcurrencyStamp",
                value: "11bf4819-634f-42ab-b3f0-6970018eb115");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 99999,
                column: "ConcurrencyStamp",
                value: "207754e5-232b-4d68-a5ec-654307ac796c");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 99999,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "64be0cf4-3f57-4d55-8150-a4c508840d0c", "AQAAAAEAACcQAAAAEHCKVdd3LYqZbotTHiZGaPyKcSJVr2HEYNOpySOlVnDyxw28Xax2JKJCOQKSr3BNug==", "ae0cc5aa-4539-430b-b5b3-78d824f5cd4c" });
        }
    }
}
