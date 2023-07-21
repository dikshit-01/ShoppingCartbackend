using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data_Access_Layer.Migrations
{
    public partial class changeColumnNamess2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FirstNamess",
                table: "AspNetUsers",
                newName: "FirstName");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2569645b-3256-49ba-ab5f-f02fc81a9527",
                column: "ConcurrencyStamp",
                value: "4baa7794-e2cb-446b-b13e-c2814fbc16cc");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f607f7ef-01df-4943-b46b-2726091e7697",
                column: "ConcurrencyStamp",
                value: "fdf06a91-9f39-4d1b-9197-09b27f3ebf28");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "e215ed86-87d2-443a-9d10-d8cd761a0a5e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d3a0233f-0e4c-48b9-9dac-367737d1c8c6", "AQAAAAEAACcQAAAAEJcJDmBtr5Z719aZ0vboAWYg00dzff4Gk4/Y3EGfWr1w0SW6wSUvPmKhmUTVF+Q+bw==", "17de7c2c-2ed4-4d5d-a668-eb2f194dda65" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FirstName",
                table: "AspNetUsers",
                newName: "FirstNamess");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2569645b-3256-49ba-ab5f-f02fc81a9527",
                column: "ConcurrencyStamp",
                value: "d687a7f3-5bf7-49a3-80d8-50f00f0acccb");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f607f7ef-01df-4943-b46b-2726091e7697",
                column: "ConcurrencyStamp",
                value: "48881b43-2c08-461c-82ad-f51286d100ec");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "e215ed86-87d2-443a-9d10-d8cd761a0a5e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "83604f3f-80fb-4062-baea-2ac85b46c912", "AQAAAAEAACcQAAAAELxpfoO6bOrgnC24U8tFi31CD5txuxHlxamwwATVDjxuLp50Q9Uauhpu0Ky0jtQ+vg==", "297bc306-4dd6-4c5c-ae46-79423257cb3b" });
        }
    }
}
