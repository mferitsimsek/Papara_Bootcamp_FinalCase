using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Papara.CaptainStore.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class mig_update_Remove_IsDeleted_From_AspNetUsers_And_Update_Date_Defaults : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "CustomerAccounts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 12, 18, 12, 38, 621, DateTimeKind.Local).AddTicks(7247),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 8, 12, 17, 58, 29, 136, DateTimeKind.Local).AddTicks(8652));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Coupons",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 12, 18, 12, 38, 623, DateTimeKind.Local).AddTicks(2007),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 8, 12, 17, 58, 29, 138, DateTimeKind.Local).AddTicks(4452));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "CustomerAccounts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 12, 17, 58, 29, 136, DateTimeKind.Local).AddTicks(8652),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 8, 12, 18, 12, 38, 621, DateTimeKind.Local).AddTicks(7247));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Coupons",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 12, 17, 58, 29, 138, DateTimeKind.Local).AddTicks(4452),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 8, 12, 18, 12, 38, 623, DateTimeKind.Local).AddTicks(2007));

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
