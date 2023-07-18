using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data_Access_Layer.Migrations
{
    public partial class mg1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "2569645b-3256-49ba-ab5f-f02fc81a9527", "9548b93a-3dca-49ed-9372-e9e667186420", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "f607f7ef-01df-4943-b46b-2726091e7697", "89786dea-e6af-4623-8c62-24e20d1dd893", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "e215ed86-87d2-443a-9d10-d8cd761a0a5e", 0, "dc487018-1819-4c0e-bfc9-72fe602db151", "admin01@gmail.com", false, "", "", false, null, "admin01@gmail.com", null, "AQAAAAEAACcQAAAAEIGkROjugmxFJKCsZd6crxmqmdZ4K7Mi2hWDhCuF5DumgNMA9wtgA4Ni8AS0xC4sfw==", "8989898989", false, "0709bed1-0ee1-4c21-817d-1a2547e77cab", false, "admin01@gmail.com" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "2569645b-3256-49ba-ab5f-f02fc81a9527", "e215ed86-87d2-443a-9d10-d8cd761a0a5e" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f607f7ef-01df-4943-b46b-2726091e7697");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "2569645b-3256-49ba-ab5f-f02fc81a9527", "e215ed86-87d2-443a-9d10-d8cd761a0a5e" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2569645b-3256-49ba-ab5f-f02fc81a9527");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "e215ed86-87d2-443a-9d10-d8cd761a0a5e");
        }
    }
}
