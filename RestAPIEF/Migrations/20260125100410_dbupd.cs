using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RestAPIEF.Migrations
{
    /// <inheritdoc />
    public partial class dbupd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Interests",
                columns: new[] { "Id", "Description", "Name", "PersonId", "Websitelink" },
                values: new object[] { 6, "On a friday evening with friends", "Bowling", 3, "www.bowling.com" });

            migrationBuilder.InsertData(
                table: "Links",
                columns: new[] { "Id", "InterestId", "Url" },
                values: new object[,]
                {
                    { 1, 2, "www.morecooking.com" },
                    { 2, 2, "www.newcooking.com" },
                    { 3, 3, "www.morereading.com" },
                    { 6, 1, "www.morehikinh.com" },
                    { 4, 6, "www.morebowling.com" },
                    { 5, 6, "www.newbowling.com" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Links",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Links",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Links",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Links",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Links",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Links",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Interests",
                keyColumn: "Id",
                keyValue: 6);
        }
    }
}
