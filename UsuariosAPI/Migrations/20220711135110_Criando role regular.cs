using Microsoft.EntityFrameworkCore.Migrations;

namespace UsuariosAPI.Migrations
{
    public partial class Criandoroleregular : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 99999,
                column: "ConcurrencyStamp",
                value: "207754e5-232b-4d68-a5ec-654307ac796c");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { 99998, "11bf4819-634f-42ab-b3f0-6970018eb115", "regular", "REGULAR" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 99999,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "64be0cf4-3f57-4d55-8150-a4c508840d0c", "AQAAAAEAACcQAAAAEHCKVdd3LYqZbotTHiZGaPyKcSJVr2HEYNOpySOlVnDyxw28Xax2JKJCOQKSr3BNug==", "ae0cc5aa-4539-430b-b5b3-78d824f5cd4c" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 99998);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 99999,
                column: "ConcurrencyStamp",
                value: "4349dc13-7c45-4f92-82de-01b2ebffdd94");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 99999,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "8f7c0c0d-8095-4a19-a375-211b5be99c3a", "AQAAAAEAACcQAAAAEKt14Fln0dPRAH2EnfYrMpQH5qNHJDWdKFmziJZ2lJWGewvnQXS95R4nNT03eQ+oXw==", "be4692e4-1f02-4611-be38-e4de8b7ed837" });
        }
    }
}
