using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lilys_CM.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class AddProductVariantOptions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OptionValueEntities_OptionEntity_OptionId",
                table: "OptionValueEntities");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OptionEntity",
                table: "OptionEntity");

            migrationBuilder.RenameTable(
                name: "OptionEntity",
                newName: "Options");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Options",
                table: "Options",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAtUtc",
                value: new DateTime(2026, 5, 3, 20, 13, 37, 112, DateTimeKind.Utc).AddTicks(4124));

            migrationBuilder.UpdateData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAtUtc",
                value: new DateTime(2026, 5, 3, 20, 13, 37, 112, DateTimeKind.Utc).AddTicks(4124));

            migrationBuilder.UpdateData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAtUtc",
                value: new DateTime(2026, 5, 3, 20, 13, 37, 112, DateTimeKind.Utc).AddTicks(4124));

            migrationBuilder.UpdateData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAtUtc",
                value: new DateTime(2026, 5, 3, 20, 13, 37, 112, DateTimeKind.Utc).AddTicks(4124));

            migrationBuilder.UpdateData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAtUtc",
                value: new DateTime(2026, 5, 3, 20, 13, 37, 112, DateTimeKind.Utc).AddTicks(4124));

            migrationBuilder.UpdateData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedAtUtc",
                value: new DateTime(2026, 5, 3, 20, 13, 37, 112, DateTimeKind.Utc).AddTicks(4124));

            migrationBuilder.UpdateData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedAtUtc",
                value: new DateTime(2026, 5, 3, 20, 13, 37, 112, DateTimeKind.Utc).AddTicks(4124));

            migrationBuilder.UpdateData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedAtUtc",
                value: new DateTime(2026, 5, 3, 20, 13, 37, 112, DateTimeKind.Utc).AddTicks(4124));

            migrationBuilder.UpdateData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 9,
                column: "CreatedAtUtc",
                value: new DateTime(2026, 5, 3, 20, 13, 37, 112, DateTimeKind.Utc).AddTicks(4124));

            migrationBuilder.UpdateData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 10,
                column: "CreatedAtUtc",
                value: new DateTime(2026, 5, 3, 20, 13, 37, 112, DateTimeKind.Utc).AddTicks(4124));

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAtUtc",
                value: new DateTime(2026, 5, 3, 20, 13, 37, 112, DateTimeKind.Utc).AddTicks(4124));

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAtUtc",
                value: new DateTime(2026, 5, 3, 20, 13, 37, 112, DateTimeKind.Utc).AddTicks(4124));

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAtUtc",
                value: new DateTime(2026, 5, 3, 20, 13, 37, 112, DateTimeKind.Utc).AddTicks(4124));

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAtUtc",
                value: new DateTime(2026, 5, 3, 20, 13, 37, 112, DateTimeKind.Utc).AddTicks(4124));

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAtUtc",
                value: new DateTime(2026, 5, 3, 20, 13, 37, 112, DateTimeKind.Utc).AddTicks(4124));

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedAtUtc",
                value: new DateTime(2026, 5, 3, 20, 13, 37, 112, DateTimeKind.Utc).AddTicks(4124));

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedAtUtc",
                value: new DateTime(2026, 5, 3, 20, 13, 37, 112, DateTimeKind.Utc).AddTicks(4124));

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedAtUtc",
                value: new DateTime(2026, 5, 3, 20, 13, 37, 112, DateTimeKind.Utc).AddTicks(4124));

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: 9,
                column: "CreatedAtUtc",
                value: new DateTime(2026, 5, 3, 20, 13, 37, 112, DateTimeKind.Utc).AddTicks(4124));

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: 10,
                column: "CreatedAtUtc",
                value: new DateTime(2026, 5, 3, 20, 13, 37, 112, DateTimeKind.Utc).AddTicks(4124));

            migrationBuilder.UpdateData(
                table: "NotificationTypes",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAtUtc",
                value: new DateTime(2026, 5, 3, 20, 13, 37, 112, DateTimeKind.Utc).AddTicks(4124));

            migrationBuilder.UpdateData(
                table: "NotificationTypes",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAtUtc",
                value: new DateTime(2026, 5, 3, 20, 13, 37, 112, DateTimeKind.Utc).AddTicks(4124));

            migrationBuilder.UpdateData(
                table: "NotificationTypes",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAtUtc",
                value: new DateTime(2026, 5, 3, 20, 13, 37, 112, DateTimeKind.Utc).AddTicks(4124));

            migrationBuilder.UpdateData(
                table: "NotificationTypes",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAtUtc",
                value: new DateTime(2026, 5, 3, 20, 13, 37, 112, DateTimeKind.Utc).AddTicks(4124));

            migrationBuilder.UpdateData(
                table: "OrderStatuses",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAtUtc",
                value: new DateTime(2026, 5, 3, 20, 13, 37, 112, DateTimeKind.Utc).AddTicks(4124));

            migrationBuilder.UpdateData(
                table: "OrderStatuses",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAtUtc",
                value: new DateTime(2026, 5, 3, 20, 13, 37, 112, DateTimeKind.Utc).AddTicks(4124));

            migrationBuilder.UpdateData(
                table: "OrderStatuses",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAtUtc",
                value: new DateTime(2026, 5, 3, 20, 13, 37, 112, DateTimeKind.Utc).AddTicks(4124));

            migrationBuilder.UpdateData(
                table: "OrderStatuses",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAtUtc",
                value: new DateTime(2026, 5, 3, 20, 13, 37, 112, DateTimeKind.Utc).AddTicks(4124));

            migrationBuilder.UpdateData(
                table: "OrderStatuses",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAtUtc",
                value: new DateTime(2026, 5, 3, 20, 13, 37, 112, DateTimeKind.Utc).AddTicks(4124));

            migrationBuilder.UpdateData(
                table: "PaymentMethods",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAtUtc",
                value: new DateTime(2026, 5, 3, 20, 13, 37, 112, DateTimeKind.Utc).AddTicks(4124));

            migrationBuilder.UpdateData(
                table: "PaymentMethods",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAtUtc",
                value: new DateTime(2026, 5, 3, 20, 13, 37, 112, DateTimeKind.Utc).AddTicks(4124));

            migrationBuilder.UpdateData(
                table: "PaymentMethods",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAtUtc",
                value: new DateTime(2026, 5, 3, 20, 13, 37, 112, DateTimeKind.Utc).AddTicks(4124));

            migrationBuilder.UpdateData(
                table: "PaymentMethods",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAtUtc",
                value: new DateTime(2026, 5, 3, 20, 13, 37, 112, DateTimeKind.Utc).AddTicks(4124));

            migrationBuilder.UpdateData(
                table: "PaymentMethods",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAtUtc",
                value: new DateTime(2026, 5, 3, 20, 13, 37, 112, DateTimeKind.Utc).AddTicks(4124));

            migrationBuilder.UpdateData(
                table: "PaymentMethods",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedAtUtc",
                value: new DateTime(2026, 5, 3, 20, 13, 37, 112, DateTimeKind.Utc).AddTicks(4124));

            migrationBuilder.UpdateData(
                table: "PaymentMethods",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedAtUtc",
                value: new DateTime(2026, 5, 3, 20, 13, 37, 112, DateTimeKind.Utc).AddTicks(4124));

            migrationBuilder.UpdateData(
                table: "PaymentMethods",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedAtUtc",
                value: new DateTime(2026, 5, 3, 20, 13, 37, 112, DateTimeKind.Utc).AddTicks(4124));

            migrationBuilder.UpdateData(
                table: "PaymentStatuses",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAtUtc",
                value: new DateTime(2026, 5, 3, 20, 13, 37, 112, DateTimeKind.Utc).AddTicks(4124));

            migrationBuilder.UpdateData(
                table: "PaymentStatuses",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAtUtc",
                value: new DateTime(2026, 5, 3, 20, 13, 37, 112, DateTimeKind.Utc).AddTicks(4124));

            migrationBuilder.UpdateData(
                table: "PaymentStatuses",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAtUtc",
                value: new DateTime(2026, 5, 3, 20, 13, 37, 112, DateTimeKind.Utc).AddTicks(4124));

            migrationBuilder.UpdateData(
                table: "PaymentStatuses",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAtUtc",
                value: new DateTime(2026, 5, 3, 20, 13, 37, 112, DateTimeKind.Utc).AddTicks(4124));

            migrationBuilder.AddForeignKey(
                name: "FK_OptionValueEntities_Options_OptionId",
                table: "OptionValueEntities",
                column: "OptionId",
                principalTable: "Options",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OptionValueEntities_Options_OptionId",
                table: "OptionValueEntities");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Options",
                table: "Options");

            migrationBuilder.RenameTable(
                name: "Options",
                newName: "OptionEntity");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OptionEntity",
                table: "OptionEntity",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAtUtc",
                value: new DateTime(2026, 5, 2, 17, 48, 3, 885, DateTimeKind.Utc).AddTicks(4521));

            migrationBuilder.UpdateData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAtUtc",
                value: new DateTime(2026, 5, 2, 17, 48, 3, 885, DateTimeKind.Utc).AddTicks(4521));

            migrationBuilder.UpdateData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAtUtc",
                value: new DateTime(2026, 5, 2, 17, 48, 3, 885, DateTimeKind.Utc).AddTicks(4521));

            migrationBuilder.UpdateData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAtUtc",
                value: new DateTime(2026, 5, 2, 17, 48, 3, 885, DateTimeKind.Utc).AddTicks(4521));

            migrationBuilder.UpdateData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAtUtc",
                value: new DateTime(2026, 5, 2, 17, 48, 3, 885, DateTimeKind.Utc).AddTicks(4521));

            migrationBuilder.UpdateData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedAtUtc",
                value: new DateTime(2026, 5, 2, 17, 48, 3, 885, DateTimeKind.Utc).AddTicks(4521));

            migrationBuilder.UpdateData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedAtUtc",
                value: new DateTime(2026, 5, 2, 17, 48, 3, 885, DateTimeKind.Utc).AddTicks(4521));

            migrationBuilder.UpdateData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedAtUtc",
                value: new DateTime(2026, 5, 2, 17, 48, 3, 885, DateTimeKind.Utc).AddTicks(4521));

            migrationBuilder.UpdateData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 9,
                column: "CreatedAtUtc",
                value: new DateTime(2026, 5, 2, 17, 48, 3, 885, DateTimeKind.Utc).AddTicks(4521));

            migrationBuilder.UpdateData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 10,
                column: "CreatedAtUtc",
                value: new DateTime(2026, 5, 2, 17, 48, 3, 885, DateTimeKind.Utc).AddTicks(4521));

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAtUtc",
                value: new DateTime(2026, 5, 2, 17, 48, 3, 885, DateTimeKind.Utc).AddTicks(4521));

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAtUtc",
                value: new DateTime(2026, 5, 2, 17, 48, 3, 885, DateTimeKind.Utc).AddTicks(4521));

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAtUtc",
                value: new DateTime(2026, 5, 2, 17, 48, 3, 885, DateTimeKind.Utc).AddTicks(4521));

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAtUtc",
                value: new DateTime(2026, 5, 2, 17, 48, 3, 885, DateTimeKind.Utc).AddTicks(4521));

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAtUtc",
                value: new DateTime(2026, 5, 2, 17, 48, 3, 885, DateTimeKind.Utc).AddTicks(4521));

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedAtUtc",
                value: new DateTime(2026, 5, 2, 17, 48, 3, 885, DateTimeKind.Utc).AddTicks(4521));

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedAtUtc",
                value: new DateTime(2026, 5, 2, 17, 48, 3, 885, DateTimeKind.Utc).AddTicks(4521));

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedAtUtc",
                value: new DateTime(2026, 5, 2, 17, 48, 3, 885, DateTimeKind.Utc).AddTicks(4521));

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: 9,
                column: "CreatedAtUtc",
                value: new DateTime(2026, 5, 2, 17, 48, 3, 885, DateTimeKind.Utc).AddTicks(4521));

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: 10,
                column: "CreatedAtUtc",
                value: new DateTime(2026, 5, 2, 17, 48, 3, 885, DateTimeKind.Utc).AddTicks(4521));

            migrationBuilder.UpdateData(
                table: "NotificationTypes",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAtUtc",
                value: new DateTime(2026, 5, 2, 17, 48, 3, 885, DateTimeKind.Utc).AddTicks(4521));

            migrationBuilder.UpdateData(
                table: "NotificationTypes",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAtUtc",
                value: new DateTime(2026, 5, 2, 17, 48, 3, 885, DateTimeKind.Utc).AddTicks(4521));

            migrationBuilder.UpdateData(
                table: "NotificationTypes",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAtUtc",
                value: new DateTime(2026, 5, 2, 17, 48, 3, 885, DateTimeKind.Utc).AddTicks(4521));

            migrationBuilder.UpdateData(
                table: "NotificationTypes",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAtUtc",
                value: new DateTime(2026, 5, 2, 17, 48, 3, 885, DateTimeKind.Utc).AddTicks(4521));

            migrationBuilder.UpdateData(
                table: "OrderStatuses",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAtUtc",
                value: new DateTime(2026, 5, 2, 17, 48, 3, 885, DateTimeKind.Utc).AddTicks(4521));

            migrationBuilder.UpdateData(
                table: "OrderStatuses",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAtUtc",
                value: new DateTime(2026, 5, 2, 17, 48, 3, 885, DateTimeKind.Utc).AddTicks(4521));

            migrationBuilder.UpdateData(
                table: "OrderStatuses",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAtUtc",
                value: new DateTime(2026, 5, 2, 17, 48, 3, 885, DateTimeKind.Utc).AddTicks(4521));

            migrationBuilder.UpdateData(
                table: "OrderStatuses",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAtUtc",
                value: new DateTime(2026, 5, 2, 17, 48, 3, 885, DateTimeKind.Utc).AddTicks(4521));

            migrationBuilder.UpdateData(
                table: "OrderStatuses",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAtUtc",
                value: new DateTime(2026, 5, 2, 17, 48, 3, 885, DateTimeKind.Utc).AddTicks(4521));

            migrationBuilder.UpdateData(
                table: "PaymentMethods",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAtUtc",
                value: new DateTime(2026, 5, 2, 17, 48, 3, 885, DateTimeKind.Utc).AddTicks(4521));

            migrationBuilder.UpdateData(
                table: "PaymentMethods",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAtUtc",
                value: new DateTime(2026, 5, 2, 17, 48, 3, 885, DateTimeKind.Utc).AddTicks(4521));

            migrationBuilder.UpdateData(
                table: "PaymentMethods",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAtUtc",
                value: new DateTime(2026, 5, 2, 17, 48, 3, 885, DateTimeKind.Utc).AddTicks(4521));

            migrationBuilder.UpdateData(
                table: "PaymentMethods",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAtUtc",
                value: new DateTime(2026, 5, 2, 17, 48, 3, 885, DateTimeKind.Utc).AddTicks(4521));

            migrationBuilder.UpdateData(
                table: "PaymentMethods",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAtUtc",
                value: new DateTime(2026, 5, 2, 17, 48, 3, 885, DateTimeKind.Utc).AddTicks(4521));

            migrationBuilder.UpdateData(
                table: "PaymentMethods",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedAtUtc",
                value: new DateTime(2026, 5, 2, 17, 48, 3, 885, DateTimeKind.Utc).AddTicks(4521));

            migrationBuilder.UpdateData(
                table: "PaymentMethods",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedAtUtc",
                value: new DateTime(2026, 5, 2, 17, 48, 3, 885, DateTimeKind.Utc).AddTicks(4521));

            migrationBuilder.UpdateData(
                table: "PaymentMethods",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedAtUtc",
                value: new DateTime(2026, 5, 2, 17, 48, 3, 885, DateTimeKind.Utc).AddTicks(4521));

            migrationBuilder.UpdateData(
                table: "PaymentStatuses",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAtUtc",
                value: new DateTime(2026, 5, 2, 17, 48, 3, 885, DateTimeKind.Utc).AddTicks(4521));

            migrationBuilder.UpdateData(
                table: "PaymentStatuses",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAtUtc",
                value: new DateTime(2026, 5, 2, 17, 48, 3, 885, DateTimeKind.Utc).AddTicks(4521));

            migrationBuilder.UpdateData(
                table: "PaymentStatuses",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAtUtc",
                value: new DateTime(2026, 5, 2, 17, 48, 3, 885, DateTimeKind.Utc).AddTicks(4521));

            migrationBuilder.UpdateData(
                table: "PaymentStatuses",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAtUtc",
                value: new DateTime(2026, 5, 2, 17, 48, 3, 885, DateTimeKind.Utc).AddTicks(4521));

            migrationBuilder.AddForeignKey(
                name: "FK_OptionValueEntities_OptionEntity_OptionId",
                table: "OptionValueEntities",
                column: "OptionId",
                principalTable: "OptionEntity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
