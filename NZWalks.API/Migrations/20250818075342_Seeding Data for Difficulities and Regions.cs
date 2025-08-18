using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace NZWalks.API.Migrations
{
    /// <inheritdoc />
    public partial class SeedingDataforDifficulitiesandRegions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Difficulties",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("67baf9d3-1f6a-4273-bbdb-0a97e42582cb"), "Easy" },
                    { new Guid("812d6ace-115c-4866-83bc-1b162dce1d5a"), "Hard" },
                    { new Guid("8d0618e1-5d64-4aa4-bb42-254767ec3752"), "Medium" }
                });

            migrationBuilder.InsertData(
                table: "Regions",
                columns: new[] { "Id", "Code", "Name", "RegionImageUrl" },
                values: new object[,]
                {
                    { new Guid("a18c7c1d-8a27-42f5-9e30-5a5cce100001"), "WLG", "Wellington", "https://www.gstatic.com/webp/gallery/5.sm.jpg" },
                    { new Guid("a18c7c1d-8a27-42f5-9e30-5a5cce100002"), "CHC", "Christchurch", "https://www.gstatic.com/webp/gallery/1.sm.jpg" },
                    { new Guid("a18c7c1d-8a27-42f5-9e30-5a5cce100003"), "HLZ", "Hamilton", "https://www.gstatic.com/webp/gallery/2.sm.jpg" },
                    { new Guid("a18c7c1d-8a27-42f5-9e30-5a5cce100004"), "TRG", "Tauranga", "https://www.gstatic.com/webp/gallery/3.sm.jpg" },
                    { new Guid("a18c7c1d-8a27-42f5-9e30-5a5cce100005"), "NPE", "Napier", "https://www.gstatic.com/webp/gallery/4.sm.jpg" },
                    { new Guid("a18c7c1d-8a27-42f5-9e30-5a5cce100006"), "DUD", "Dunedin", "https://www.gstatic.com/webp/gallery/5.sm.jpg" },
                    { new Guid("a18c7c1d-8a27-42f5-9e30-5a5cce100007"), "PMR", "Palmerston North", "https://www.gstatic.com/webp/gallery/1.sm.jpg" },
                    { new Guid("a18c7c1d-8a27-42f5-9e30-5a5cce100008"), "NSN", "Nelson", "https://www.gstatic.com/webp/gallery/2.sm.jpg" },
                    { new Guid("a18c7c1d-8a27-42f5-9e30-5a5cce100009"), "ROT", "Rotorua", "https://www.gstatic.com/webp/gallery/3.sm.jpg" },
                    { new Guid("a18c7c1d-8a27-42f5-9e30-5a5cce100010"), "NPL", "New Plymouth", "https://www.gstatic.com/webp/gallery/4.sm.jpg" },
                    { new Guid("a18c7c1d-8a27-42f5-9e30-5a5cce100011"), "IVC", "Invercargill", "https://www.gstatic.com/webp/gallery/5.sm.jpg" },
                    { new Guid("a18c7c1d-8a27-42f5-9e30-5a5cce100012"), "WRE", "Whangarei", "https://www.gstatic.com/webp/gallery/1.sm.jpg" },
                    { new Guid("a18c7c1d-8a27-42f5-9e30-5a5cce100013"), "GIS", "Gisborne", "https://www.gstatic.com/webp/gallery/2.sm.jpg" },
                    { new Guid("a18c7c1d-8a27-42f5-9e30-5a5cce100014"), "TIU", "Timaru", "https://www.gstatic.com/webp/gallery/3.sm.jpg" },
                    { new Guid("a18c7c1d-8a27-42f5-9e30-5a5cce100015"), "ZQN", "Queenstown", "https://www.gstatic.com/webp/gallery/4.sm.jpg" },
                    { new Guid("a18c7c1d-8a27-42f5-9e30-5a5cce100016"), "MST", "Masterton", "https://www.gstatic.com/webp/gallery/5.sm.jpg" },
                    { new Guid("a18c7c1d-8a27-42f5-9e30-5a5cce100017"), "KKR", "Kaikoura", "https://www.gstatic.com/webp/gallery/1.sm.jpg" },
                    { new Guid("a18c7c1d-8a27-42f5-9e30-5a5cce100018"), "TUO", "Taupo", "https://www.gstatic.com/webp/gallery/2.sm.jpg" },
                    { new Guid("e7044cdb-f20f-4a90-893a-e357f2aecdf0"), "AKL", "Auckland", "https://www.gstatic.com/webp/gallery/4.sm.jpg" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("67baf9d3-1f6a-4273-bbdb-0a97e42582cb"));

            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("812d6ace-115c-4866-83bc-1b162dce1d5a"));

            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("8d0618e1-5d64-4aa4-bb42-254767ec3752"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("a18c7c1d-8a27-42f5-9e30-5a5cce100001"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("a18c7c1d-8a27-42f5-9e30-5a5cce100002"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("a18c7c1d-8a27-42f5-9e30-5a5cce100003"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("a18c7c1d-8a27-42f5-9e30-5a5cce100004"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("a18c7c1d-8a27-42f5-9e30-5a5cce100005"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("a18c7c1d-8a27-42f5-9e30-5a5cce100006"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("a18c7c1d-8a27-42f5-9e30-5a5cce100007"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("a18c7c1d-8a27-42f5-9e30-5a5cce100008"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("a18c7c1d-8a27-42f5-9e30-5a5cce100009"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("a18c7c1d-8a27-42f5-9e30-5a5cce100010"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("a18c7c1d-8a27-42f5-9e30-5a5cce100011"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("a18c7c1d-8a27-42f5-9e30-5a5cce100012"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("a18c7c1d-8a27-42f5-9e30-5a5cce100013"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("a18c7c1d-8a27-42f5-9e30-5a5cce100014"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("a18c7c1d-8a27-42f5-9e30-5a5cce100015"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("a18c7c1d-8a27-42f5-9e30-5a5cce100016"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("a18c7c1d-8a27-42f5-9e30-5a5cce100017"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("a18c7c1d-8a27-42f5-9e30-5a5cce100018"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("e7044cdb-f20f-4a90-893a-e357f2aecdf0"));
        }
    }
}
