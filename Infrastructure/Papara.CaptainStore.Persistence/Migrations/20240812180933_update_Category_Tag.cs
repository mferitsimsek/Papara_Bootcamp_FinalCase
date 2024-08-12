using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Papara.CaptainStore.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class update_Category_Tag : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "CustomerAccounts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 12, 21, 9, 32, 986, DateTimeKind.Local).AddTicks(6474),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 8, 12, 18, 12, 38, 621, DateTimeKind.Local).AddTicks(7247));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Coupons",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 12, 21, 9, 32, 988, DateTimeKind.Local).AddTicks(6806),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 8, 12, 18, 12, 38, 623, DateTimeKind.Local).AddTicks(2007));

            migrationBuilder.AlterColumn<string>(
                name: "Tag",
                table: "Categories",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(3)",
                oldMaxLength: 3);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "CustomerAccounts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 12, 18, 12, 38, 621, DateTimeKind.Local).AddTicks(7247),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 8, 12, 21, 9, 32, 986, DateTimeKind.Local).AddTicks(6474));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Coupons",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 12, 18, 12, 38, 623, DateTimeKind.Local).AddTicks(2007),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 8, 12, 21, 9, 32, 988, DateTimeKind.Local).AddTicks(6806));

            migrationBuilder.AlterColumn<string>(
                name: "Tag",
                table: "Categories",
                type: "nvarchar(3)",
                maxLength: 3,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);
        }
    }
}
