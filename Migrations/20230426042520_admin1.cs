using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace mobile_recharger.Migrations
{
    public partial class admin1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e575",
                column: "ConcurrencyStamp",
                value: "8fe22c91-eac5-433d-8dc6-3edccdbb3340");

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "User",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e575",
                columns: new[] { "ConcurrencyStamp", "NormalizedUserName", "PasswordHash", "UserName" },
                values: new object[] { "423fe1fe-aba2-4773-b0f7-247840005c02", "admin@mail.com", "AQAAAAEAACcQAAAAEFt8UdsdsraoU3w5Mmw5OBGjOnpsIgzq60JC33ZC+cZ0YokXsy+RtuE3sishPJjjNA==", "admin@mail.com" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e575",
                column: "ConcurrencyStamp",
                value: "ee5d57b6-38ae-4226-8464-2d5cbc855351");

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "User",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e575",
                columns: new[] { "ConcurrencyStamp", "NormalizedUserName", "PasswordHash", "UserName" },
                values: new object[] { "84d8b53c-0b22-4a3b-ac6d-e238cc204600", "admin", "AQAAAAEAACcQAAAAECOLubT+tJJ0jW8zXkhCCTH9Z7OUMSm0OBnXdFC7wVTxT5eK+EZGAOhupTFH/dXHqw==", "admin" });
        }
    }
}
