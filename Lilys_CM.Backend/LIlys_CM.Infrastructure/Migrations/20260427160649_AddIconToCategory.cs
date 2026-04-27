using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lilys_CM.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddIconToCategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Icon",
                table: "Categories",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAtUtc",
                value: new DateTime(2026, 4, 27, 16, 6, 46, 109, DateTimeKind.Utc).AddTicks(2922));

            migrationBuilder.UpdateData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAtUtc",
                value: new DateTime(2026, 4, 27, 16, 6, 46, 109, DateTimeKind.Utc).AddTicks(2922));

            migrationBuilder.UpdateData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAtUtc",
                value: new DateTime(2026, 4, 27, 16, 6, 46, 109, DateTimeKind.Utc).AddTicks(2922));

            migrationBuilder.UpdateData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAtUtc",
                value: new DateTime(2026, 4, 27, 16, 6, 46, 109, DateTimeKind.Utc).AddTicks(2922));

            migrationBuilder.UpdateData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAtUtc",
                value: new DateTime(2026, 4, 27, 16, 6, 46, 109, DateTimeKind.Utc).AddTicks(2922));

            migrationBuilder.UpdateData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedAtUtc",
                value: new DateTime(2026, 4, 27, 16, 6, 46, 109, DateTimeKind.Utc).AddTicks(2922));

            migrationBuilder.UpdateData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedAtUtc",
                value: new DateTime(2026, 4, 27, 16, 6, 46, 109, DateTimeKind.Utc).AddTicks(2922));

            migrationBuilder.UpdateData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedAtUtc",
                value: new DateTime(2026, 4, 27, 16, 6, 46, 109, DateTimeKind.Utc).AddTicks(2922));

            migrationBuilder.UpdateData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 9,
                column: "CreatedAtUtc",
                value: new DateTime(2026, 4, 27, 16, 6, 46, 109, DateTimeKind.Utc).AddTicks(2922));

            migrationBuilder.UpdateData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 10,
                column: "CreatedAtUtc",
                value: new DateTime(2026, 4, 27, 16, 6, 46, 109, DateTimeKind.Utc).AddTicks(2922));

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAtUtc",
                value: new DateTime(2026, 4, 27, 16, 6, 46, 109, DateTimeKind.Utc).AddTicks(2922));

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAtUtc",
                value: new DateTime(2026, 4, 27, 16, 6, 46, 109, DateTimeKind.Utc).AddTicks(2922));

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAtUtc",
                value: new DateTime(2026, 4, 27, 16, 6, 46, 109, DateTimeKind.Utc).AddTicks(2922));

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAtUtc",
                value: new DateTime(2026, 4, 27, 16, 6, 46, 109, DateTimeKind.Utc).AddTicks(2922));

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAtUtc",
                value: new DateTime(2026, 4, 27, 16, 6, 46, 109, DateTimeKind.Utc).AddTicks(2922));

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedAtUtc",
                value: new DateTime(2026, 4, 27, 16, 6, 46, 109, DateTimeKind.Utc).AddTicks(2922));

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedAtUtc",
                value: new DateTime(2026, 4, 27, 16, 6, 46, 109, DateTimeKind.Utc).AddTicks(2922));

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedAtUtc",
                value: new DateTime(2026, 4, 27, 16, 6, 46, 109, DateTimeKind.Utc).AddTicks(2922));

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: 9,
                column: "CreatedAtUtc",
                value: new DateTime(2026, 4, 27, 16, 6, 46, 109, DateTimeKind.Utc).AddTicks(2922));

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: 10,
                column: "CreatedAtUtc",
                value: new DateTime(2026, 4, 27, 16, 6, 46, 109, DateTimeKind.Utc).AddTicks(2922));

            migrationBuilder.UpdateData(
                table: "NotificationTypes",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAtUtc",
                value: new DateTime(2026, 4, 27, 16, 6, 46, 109, DateTimeKind.Utc).AddTicks(2922));

            migrationBuilder.UpdateData(
                table: "NotificationTypes",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAtUtc",
                value: new DateTime(2026, 4, 27, 16, 6, 46, 109, DateTimeKind.Utc).AddTicks(2922));

            migrationBuilder.UpdateData(
                table: "NotificationTypes",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAtUtc",
                value: new DateTime(2026, 4, 27, 16, 6, 46, 109, DateTimeKind.Utc).AddTicks(2922));

            migrationBuilder.UpdateData(
                table: "NotificationTypes",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAtUtc",
                value: new DateTime(2026, 4, 27, 16, 6, 46, 109, DateTimeKind.Utc).AddTicks(2922));

            migrationBuilder.UpdateData(
                table: "OrderStatuses",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAtUtc",
                value: new DateTime(2026, 4, 27, 16, 6, 46, 109, DateTimeKind.Utc).AddTicks(2922));

            migrationBuilder.UpdateData(
                table: "OrderStatuses",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAtUtc",
                value: new DateTime(2026, 4, 27, 16, 6, 46, 109, DateTimeKind.Utc).AddTicks(2922));

            migrationBuilder.UpdateData(
                table: "OrderStatuses",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAtUtc",
                value: new DateTime(2026, 4, 27, 16, 6, 46, 109, DateTimeKind.Utc).AddTicks(2922));

            migrationBuilder.UpdateData(
                table: "OrderStatuses",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAtUtc",
                value: new DateTime(2026, 4, 27, 16, 6, 46, 109, DateTimeKind.Utc).AddTicks(2922));

            migrationBuilder.UpdateData(
                table: "OrderStatuses",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAtUtc",
                value: new DateTime(2026, 4, 27, 16, 6, 46, 109, DateTimeKind.Utc).AddTicks(2922));

            migrationBuilder.UpdateData(
                table: "PaymentMethods",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAtUtc",
                value: new DateTime(2026, 4, 27, 16, 6, 46, 109, DateTimeKind.Utc).AddTicks(2922));

            migrationBuilder.UpdateData(
                table: "PaymentMethods",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAtUtc",
                value: new DateTime(2026, 4, 27, 16, 6, 46, 109, DateTimeKind.Utc).AddTicks(2922));

            migrationBuilder.UpdateData(
                table: "PaymentMethods",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAtUtc",
                value: new DateTime(2026, 4, 27, 16, 6, 46, 109, DateTimeKind.Utc).AddTicks(2922));

            migrationBuilder.UpdateData(
                table: "PaymentMethods",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAtUtc",
                value: new DateTime(2026, 4, 27, 16, 6, 46, 109, DateTimeKind.Utc).AddTicks(2922));

            migrationBuilder.UpdateData(
                table: "PaymentMethods",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAtUtc",
                value: new DateTime(2026, 4, 27, 16, 6, 46, 109, DateTimeKind.Utc).AddTicks(2922));

            migrationBuilder.UpdateData(
                table: "PaymentMethods",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedAtUtc",
                value: new DateTime(2026, 4, 27, 16, 6, 46, 109, DateTimeKind.Utc).AddTicks(2922));

            migrationBuilder.UpdateData(
                table: "PaymentMethods",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedAtUtc",
                value: new DateTime(2026, 4, 27, 16, 6, 46, 109, DateTimeKind.Utc).AddTicks(2922));

            migrationBuilder.UpdateData(
                table: "PaymentMethods",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedAtUtc",
                value: new DateTime(2026, 4, 27, 16, 6, 46, 109, DateTimeKind.Utc).AddTicks(2922));

            migrationBuilder.UpdateData(
                table: "PaymentStatuses",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAtUtc",
                value: new DateTime(2026, 4, 27, 16, 6, 46, 109, DateTimeKind.Utc).AddTicks(2922));

            migrationBuilder.UpdateData(
                table: "PaymentStatuses",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAtUtc",
                value: new DateTime(2026, 4, 27, 16, 6, 46, 109, DateTimeKind.Utc).AddTicks(2922));

            migrationBuilder.UpdateData(
                table: "PaymentStatuses",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAtUtc",
                value: new DateTime(2026, 4, 27, 16, 6, 46, 109, DateTimeKind.Utc).AddTicks(2922));

            migrationBuilder.UpdateData(
                table: "PaymentStatuses",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAtUtc",
                value: new DateTime(2026, 4, 27, 16, 6, 46, 109, DateTimeKind.Utc).AddTicks(2922));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Icon",
                table: "Categories");

            migrationBuilder.UpdateData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAtUtc",
                value: new DateTime(2026, 4, 27, 15, 49, 8, 491, DateTimeKind.Utc).AddTicks(9936));

            migrationBuilder.UpdateData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAtUtc",
                value: new DateTime(2026, 4, 27, 15, 49, 8, 491, DateTimeKind.Utc).AddTicks(9936));

            migrationBuilder.UpdateData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAtUtc",
                value: new DateTime(2026, 4, 27, 15, 49, 8, 491, DateTimeKind.Utc).AddTicks(9936));

            migrationBuilder.UpdateData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAtUtc",
                value: new DateTime(2026, 4, 27, 15, 49, 8, 491, DateTimeKind.Utc).AddTicks(9936));

            migrationBuilder.UpdateData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAtUtc",
                value: new DateTime(2026, 4, 27, 15, 49, 8, 491, DateTimeKind.Utc).AddTicks(9936));

            migrationBuilder.UpdateData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedAtUtc",
                value: new DateTime(2026, 4, 27, 15, 49, 8, 491, DateTimeKind.Utc).AddTicks(9936));

            migrationBuilder.UpdateData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedAtUtc",
                value: new DateTime(2026, 4, 27, 15, 49, 8, 491, DateTimeKind.Utc).AddTicks(9936));

            migrationBuilder.UpdateData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedAtUtc",
                value: new DateTime(2026, 4, 27, 15, 49, 8, 491, DateTimeKind.Utc).AddTicks(9936));

            migrationBuilder.UpdateData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 9,
                column: "CreatedAtUtc",
                value: new DateTime(2026, 4, 27, 15, 49, 8, 491, DateTimeKind.Utc).AddTicks(9936));

            migrationBuilder.UpdateData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 10,
                column: "CreatedAtUtc",
                value: new DateTime(2026, 4, 27, 15, 49, 8, 491, DateTimeKind.Utc).AddTicks(9936));

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAtUtc",
                value: new DateTime(2026, 4, 27, 15, 49, 8, 491, DateTimeKind.Utc).AddTicks(9936));

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAtUtc",
                value: new DateTime(2026, 4, 27, 15, 49, 8, 491, DateTimeKind.Utc).AddTicks(9936));

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAtUtc",
                value: new DateTime(2026, 4, 27, 15, 49, 8, 491, DateTimeKind.Utc).AddTicks(9936));

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAtUtc",
                value: new DateTime(2026, 4, 27, 15, 49, 8, 491, DateTimeKind.Utc).AddTicks(9936));

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAtUtc",
                value: new DateTime(2026, 4, 27, 15, 49, 8, 491, DateTimeKind.Utc).AddTicks(9936));

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedAtUtc",
                value: new DateTime(2026, 4, 27, 15, 49, 8, 491, DateTimeKind.Utc).AddTicks(9936));

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedAtUtc",
                value: new DateTime(2026, 4, 27, 15, 49, 8, 491, DateTimeKind.Utc).AddTicks(9936));

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedAtUtc",
                value: new DateTime(2026, 4, 27, 15, 49, 8, 491, DateTimeKind.Utc).AddTicks(9936));

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: 9,
                column: "CreatedAtUtc",
                value: new DateTime(2026, 4, 27, 15, 49, 8, 491, DateTimeKind.Utc).AddTicks(9936));

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: 10,
                column: "CreatedAtUtc",
                value: new DateTime(2026, 4, 27, 15, 49, 8, 491, DateTimeKind.Utc).AddTicks(9936));

            migrationBuilder.UpdateData(
                table: "NotificationTypes",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAtUtc",
                value: new DateTime(2026, 4, 27, 15, 49, 8, 491, DateTimeKind.Utc).AddTicks(9936));

            migrationBuilder.UpdateData(
                table: "NotificationTypes",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAtUtc",
                value: new DateTime(2026, 4, 27, 15, 49, 8, 491, DateTimeKind.Utc).AddTicks(9936));

            migrationBuilder.UpdateData(
                table: "NotificationTypes",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAtUtc",
                value: new DateTime(2026, 4, 27, 15, 49, 8, 491, DateTimeKind.Utc).AddTicks(9936));

            migrationBuilder.UpdateData(
                table: "NotificationTypes",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAtUtc",
                value: new DateTime(2026, 4, 27, 15, 49, 8, 491, DateTimeKind.Utc).AddTicks(9936));

            migrationBuilder.UpdateData(
                table: "OrderStatuses",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAtUtc",
                value: new DateTime(2026, 4, 27, 15, 49, 8, 491, DateTimeKind.Utc).AddTicks(9936));

            migrationBuilder.UpdateData(
                table: "OrderStatuses",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAtUtc",
                value: new DateTime(2026, 4, 27, 15, 49, 8, 491, DateTimeKind.Utc).AddTicks(9936));

            migrationBuilder.UpdateData(
                table: "OrderStatuses",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAtUtc",
                value: new DateTime(2026, 4, 27, 15, 49, 8, 491, DateTimeKind.Utc).AddTicks(9936));

            migrationBuilder.UpdateData(
                table: "OrderStatuses",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAtUtc",
                value: new DateTime(2026, 4, 27, 15, 49, 8, 491, DateTimeKind.Utc).AddTicks(9936));

            migrationBuilder.UpdateData(
                table: "OrderStatuses",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAtUtc",
                value: new DateTime(2026, 4, 27, 15, 49, 8, 491, DateTimeKind.Utc).AddTicks(9936));

            migrationBuilder.UpdateData(
                table: "PaymentMethods",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAtUtc",
                value: new DateTime(2026, 4, 27, 15, 49, 8, 491, DateTimeKind.Utc).AddTicks(9936));

            migrationBuilder.UpdateData(
                table: "PaymentMethods",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAtUtc",
                value: new DateTime(2026, 4, 27, 15, 49, 8, 491, DateTimeKind.Utc).AddTicks(9936));

            migrationBuilder.UpdateData(
                table: "PaymentMethods",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAtUtc",
                value: new DateTime(2026, 4, 27, 15, 49, 8, 491, DateTimeKind.Utc).AddTicks(9936));

            migrationBuilder.UpdateData(
                table: "PaymentMethods",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAtUtc",
                value: new DateTime(2026, 4, 27, 15, 49, 8, 491, DateTimeKind.Utc).AddTicks(9936));

            migrationBuilder.UpdateData(
                table: "PaymentMethods",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAtUtc",
                value: new DateTime(2026, 4, 27, 15, 49, 8, 491, DateTimeKind.Utc).AddTicks(9936));

            migrationBuilder.UpdateData(
                table: "PaymentMethods",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedAtUtc",
                value: new DateTime(2026, 4, 27, 15, 49, 8, 491, DateTimeKind.Utc).AddTicks(9936));

            migrationBuilder.UpdateData(
                table: "PaymentMethods",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedAtUtc",
                value: new DateTime(2026, 4, 27, 15, 49, 8, 491, DateTimeKind.Utc).AddTicks(9936));

            migrationBuilder.UpdateData(
                table: "PaymentMethods",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedAtUtc",
                value: new DateTime(2026, 4, 27, 15, 49, 8, 491, DateTimeKind.Utc).AddTicks(9936));

            migrationBuilder.UpdateData(
                table: "PaymentStatuses",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAtUtc",
                value: new DateTime(2026, 4, 27, 15, 49, 8, 491, DateTimeKind.Utc).AddTicks(9936));

            migrationBuilder.UpdateData(
                table: "PaymentStatuses",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAtUtc",
                value: new DateTime(2026, 4, 27, 15, 49, 8, 491, DateTimeKind.Utc).AddTicks(9936));

            migrationBuilder.UpdateData(
                table: "PaymentStatuses",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAtUtc",
                value: new DateTime(2026, 4, 27, 15, 49, 8, 491, DateTimeKind.Utc).AddTicks(9936));

            migrationBuilder.UpdateData(
                table: "PaymentStatuses",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAtUtc",
                value: new DateTime(2026, 4, 27, 15, 49, 8, 491, DateTimeKind.Utc).AddTicks(9936));
        }
    }
}
