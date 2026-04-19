using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lilys_CM.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ProductCrudSetup : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Countries_Courencies_CurrencyId",
                table: "Countries");

            migrationBuilder.DropForeignKey(
                name: "FK_PaymentTransactions_Courencies_CurrencyId",
                table: "PaymentTransactions");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Subcategories_SubcategoryId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_VariantOptionEntitie_OptionValueEntities_OptionValueId",
                table: "VariantOptionEntitie");

            migrationBuilder.DropForeignKey(
                name: "FK_VariantOptionEntitie_ProductVariants_VariantId",
                table: "VariantOptionEntitie");

            migrationBuilder.DropIndex(
                name: "IX_Products_SubcategoryId",
                table: "Products");

            migrationBuilder.DropPrimaryKey(
                name: "PK_VariantOptionEntitie",
                table: "VariantOptionEntitie");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Courencies",
                table: "Courencies");

            migrationBuilder.DropColumn(
                name: "SubcategoryId",
                table: "Products");

            migrationBuilder.RenameTable(
                name: "VariantOptionEntitie",
                newName: "VariantOptionEntities");

            migrationBuilder.RenameTable(
                name: "Courencies",
                newName: "Currencies");

            migrationBuilder.RenameIndex(
                name: "IX_VariantOptionEntitie_VariantId",
                table: "VariantOptionEntities",
                newName: "IX_VariantOptionEntities_VariantId");

            migrationBuilder.RenameIndex(
                name: "IX_VariantOptionEntitie_OptionValueId",
                table: "VariantOptionEntities",
                newName: "IX_VariantOptionEntities_OptionValueId");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsEnabled",
                table: "Products",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "Products",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "StockQuantity",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_VariantOptionEntities",
                table: "VariantOptionEntities",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Currencies",
                table: "Currencies",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAtUtc",
                value: new DateTime(2026, 4, 11, 21, 11, 32, 528, DateTimeKind.Utc).AddTicks(8647));

            migrationBuilder.UpdateData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAtUtc",
                value: new DateTime(2026, 4, 11, 21, 11, 32, 528, DateTimeKind.Utc).AddTicks(8647));

            migrationBuilder.UpdateData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAtUtc",
                value: new DateTime(2026, 4, 11, 21, 11, 32, 528, DateTimeKind.Utc).AddTicks(8647));

            migrationBuilder.UpdateData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAtUtc",
                value: new DateTime(2026, 4, 11, 21, 11, 32, 528, DateTimeKind.Utc).AddTicks(8647));

            migrationBuilder.UpdateData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAtUtc",
                value: new DateTime(2026, 4, 11, 21, 11, 32, 528, DateTimeKind.Utc).AddTicks(8647));

            migrationBuilder.UpdateData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedAtUtc",
                value: new DateTime(2026, 4, 11, 21, 11, 32, 528, DateTimeKind.Utc).AddTicks(8647));

            migrationBuilder.UpdateData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedAtUtc",
                value: new DateTime(2026, 4, 11, 21, 11, 32, 528, DateTimeKind.Utc).AddTicks(8647));

            migrationBuilder.UpdateData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedAtUtc",
                value: new DateTime(2026, 4, 11, 21, 11, 32, 528, DateTimeKind.Utc).AddTicks(8647));

            migrationBuilder.UpdateData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 9,
                column: "CreatedAtUtc",
                value: new DateTime(2026, 4, 11, 21, 11, 32, 528, DateTimeKind.Utc).AddTicks(8647));

            migrationBuilder.UpdateData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 10,
                column: "CreatedAtUtc",
                value: new DateTime(2026, 4, 11, 21, 11, 32, 528, DateTimeKind.Utc).AddTicks(8647));

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAtUtc",
                value: new DateTime(2026, 4, 11, 21, 11, 32, 528, DateTimeKind.Utc).AddTicks(8647));

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAtUtc",
                value: new DateTime(2026, 4, 11, 21, 11, 32, 528, DateTimeKind.Utc).AddTicks(8647));

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAtUtc",
                value: new DateTime(2026, 4, 11, 21, 11, 32, 528, DateTimeKind.Utc).AddTicks(8647));

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAtUtc",
                value: new DateTime(2026, 4, 11, 21, 11, 32, 528, DateTimeKind.Utc).AddTicks(8647));

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAtUtc",
                value: new DateTime(2026, 4, 11, 21, 11, 32, 528, DateTimeKind.Utc).AddTicks(8647));

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedAtUtc",
                value: new DateTime(2026, 4, 11, 21, 11, 32, 528, DateTimeKind.Utc).AddTicks(8647));

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedAtUtc",
                value: new DateTime(2026, 4, 11, 21, 11, 32, 528, DateTimeKind.Utc).AddTicks(8647));

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedAtUtc",
                value: new DateTime(2026, 4, 11, 21, 11, 32, 528, DateTimeKind.Utc).AddTicks(8647));

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: 9,
                column: "CreatedAtUtc",
                value: new DateTime(2026, 4, 11, 21, 11, 32, 528, DateTimeKind.Utc).AddTicks(8647));

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: 10,
                column: "CreatedAtUtc",
                value: new DateTime(2026, 4, 11, 21, 11, 32, 528, DateTimeKind.Utc).AddTicks(8647));

            migrationBuilder.UpdateData(
                table: "NotificationTypes",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAtUtc",
                value: new DateTime(2026, 4, 11, 21, 11, 32, 528, DateTimeKind.Utc).AddTicks(8647));

            migrationBuilder.UpdateData(
                table: "NotificationTypes",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAtUtc",
                value: new DateTime(2026, 4, 11, 21, 11, 32, 528, DateTimeKind.Utc).AddTicks(8647));

            migrationBuilder.UpdateData(
                table: "NotificationTypes",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAtUtc",
                value: new DateTime(2026, 4, 11, 21, 11, 32, 528, DateTimeKind.Utc).AddTicks(8647));

            migrationBuilder.UpdateData(
                table: "NotificationTypes",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAtUtc",
                value: new DateTime(2026, 4, 11, 21, 11, 32, 528, DateTimeKind.Utc).AddTicks(8647));

            migrationBuilder.UpdateData(
                table: "OrderStatuses",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAtUtc",
                value: new DateTime(2026, 4, 11, 21, 11, 32, 528, DateTimeKind.Utc).AddTicks(8647));

            migrationBuilder.UpdateData(
                table: "OrderStatuses",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAtUtc",
                value: new DateTime(2026, 4, 11, 21, 11, 32, 528, DateTimeKind.Utc).AddTicks(8647));

            migrationBuilder.UpdateData(
                table: "OrderStatuses",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAtUtc",
                value: new DateTime(2026, 4, 11, 21, 11, 32, 528, DateTimeKind.Utc).AddTicks(8647));

            migrationBuilder.UpdateData(
                table: "OrderStatuses",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAtUtc",
                value: new DateTime(2026, 4, 11, 21, 11, 32, 528, DateTimeKind.Utc).AddTicks(8647));

            migrationBuilder.UpdateData(
                table: "OrderStatuses",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAtUtc",
                value: new DateTime(2026, 4, 11, 21, 11, 32, 528, DateTimeKind.Utc).AddTicks(8647));

            migrationBuilder.UpdateData(
                table: "PaymentMethods",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAtUtc",
                value: new DateTime(2026, 4, 11, 21, 11, 32, 528, DateTimeKind.Utc).AddTicks(8647));

            migrationBuilder.UpdateData(
                table: "PaymentMethods",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAtUtc",
                value: new DateTime(2026, 4, 11, 21, 11, 32, 528, DateTimeKind.Utc).AddTicks(8647));

            migrationBuilder.UpdateData(
                table: "PaymentMethods",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAtUtc",
                value: new DateTime(2026, 4, 11, 21, 11, 32, 528, DateTimeKind.Utc).AddTicks(8647));

            migrationBuilder.UpdateData(
                table: "PaymentMethods",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAtUtc",
                value: new DateTime(2026, 4, 11, 21, 11, 32, 528, DateTimeKind.Utc).AddTicks(8647));

            migrationBuilder.UpdateData(
                table: "PaymentMethods",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAtUtc",
                value: new DateTime(2026, 4, 11, 21, 11, 32, 528, DateTimeKind.Utc).AddTicks(8647));

            migrationBuilder.UpdateData(
                table: "PaymentMethods",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedAtUtc",
                value: new DateTime(2026, 4, 11, 21, 11, 32, 528, DateTimeKind.Utc).AddTicks(8647));

            migrationBuilder.UpdateData(
                table: "PaymentMethods",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedAtUtc",
                value: new DateTime(2026, 4, 11, 21, 11, 32, 528, DateTimeKind.Utc).AddTicks(8647));

            migrationBuilder.UpdateData(
                table: "PaymentMethods",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedAtUtc",
                value: new DateTime(2026, 4, 11, 21, 11, 32, 528, DateTimeKind.Utc).AddTicks(8647));

            migrationBuilder.UpdateData(
                table: "PaymentStatuses",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAtUtc",
                value: new DateTime(2026, 4, 11, 21, 11, 32, 528, DateTimeKind.Utc).AddTicks(8647));

            migrationBuilder.UpdateData(
                table: "PaymentStatuses",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAtUtc",
                value: new DateTime(2026, 4, 11, 21, 11, 32, 528, DateTimeKind.Utc).AddTicks(8647));

            migrationBuilder.UpdateData(
                table: "PaymentStatuses",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAtUtc",
                value: new DateTime(2026, 4, 11, 21, 11, 32, 528, DateTimeKind.Utc).AddTicks(8647));

            migrationBuilder.UpdateData(
                table: "PaymentStatuses",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAtUtc",
                value: new DateTime(2026, 4, 11, 21, 11, 32, 528, DateTimeKind.Utc).AddTicks(8647));

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Countries_Currencies_CurrencyId",
                table: "Countries",
                column: "CurrencyId",
                principalTable: "Currencies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentTransactions_Currencies_CurrencyId",
                table: "PaymentTransactions",
                column: "CurrencyId",
                principalTable: "Currencies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Categories_CategoryId",
                table: "Products",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_VariantOptionEntities_OptionValueEntities_OptionValueId",
                table: "VariantOptionEntities",
                column: "OptionValueId",
                principalTable: "OptionValueEntities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_VariantOptionEntities_ProductVariants_VariantId",
                table: "VariantOptionEntities",
                column: "VariantId",
                principalTable: "ProductVariants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Countries_Currencies_CurrencyId",
                table: "Countries");

            migrationBuilder.DropForeignKey(
                name: "FK_PaymentTransactions_Currencies_CurrencyId",
                table: "PaymentTransactions");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Categories_CategoryId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_VariantOptionEntities_OptionValueEntities_OptionValueId",
                table: "VariantOptionEntities");

            migrationBuilder.DropForeignKey(
                name: "FK_VariantOptionEntities_ProductVariants_VariantId",
                table: "VariantOptionEntities");

            migrationBuilder.DropIndex(
                name: "IX_Products_CategoryId",
                table: "Products");

            migrationBuilder.DropPrimaryKey(
                name: "PK_VariantOptionEntities",
                table: "VariantOptionEntities");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Currencies",
                table: "Currencies");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "IsEnabled",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "StockQuantity",
                table: "Products");

            migrationBuilder.RenameTable(
                name: "VariantOptionEntities",
                newName: "VariantOptionEntitie");

            migrationBuilder.RenameTable(
                name: "Currencies",
                newName: "Courencies");

            migrationBuilder.RenameIndex(
                name: "IX_VariantOptionEntities_VariantId",
                table: "VariantOptionEntitie",
                newName: "IX_VariantOptionEntitie_VariantId");

            migrationBuilder.RenameIndex(
                name: "IX_VariantOptionEntities_OptionValueId",
                table: "VariantOptionEntitie",
                newName: "IX_VariantOptionEntitie_OptionValueId");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SubcategoryId",
                table: "Products",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_VariantOptionEntitie",
                table: "VariantOptionEntitie",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Courencies",
                table: "Courencies",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAtUtc",
                value: new DateTime(2025, 11, 7, 21, 1, 1, 838, DateTimeKind.Utc).AddTicks(7465));

            migrationBuilder.UpdateData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAtUtc",
                value: new DateTime(2025, 11, 7, 21, 1, 1, 838, DateTimeKind.Utc).AddTicks(7465));

            migrationBuilder.UpdateData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAtUtc",
                value: new DateTime(2025, 11, 7, 21, 1, 1, 838, DateTimeKind.Utc).AddTicks(7465));

            migrationBuilder.UpdateData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAtUtc",
                value: new DateTime(2025, 11, 7, 21, 1, 1, 838, DateTimeKind.Utc).AddTicks(7465));

            migrationBuilder.UpdateData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAtUtc",
                value: new DateTime(2025, 11, 7, 21, 1, 1, 838, DateTimeKind.Utc).AddTicks(7465));

            migrationBuilder.UpdateData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedAtUtc",
                value: new DateTime(2025, 11, 7, 21, 1, 1, 838, DateTimeKind.Utc).AddTicks(7465));

            migrationBuilder.UpdateData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedAtUtc",
                value: new DateTime(2025, 11, 7, 21, 1, 1, 838, DateTimeKind.Utc).AddTicks(7465));

            migrationBuilder.UpdateData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedAtUtc",
                value: new DateTime(2025, 11, 7, 21, 1, 1, 838, DateTimeKind.Utc).AddTicks(7465));

            migrationBuilder.UpdateData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 9,
                column: "CreatedAtUtc",
                value: new DateTime(2025, 11, 7, 21, 1, 1, 838, DateTimeKind.Utc).AddTicks(7465));

            migrationBuilder.UpdateData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 10,
                column: "CreatedAtUtc",
                value: new DateTime(2025, 11, 7, 21, 1, 1, 838, DateTimeKind.Utc).AddTicks(7465));

            migrationBuilder.UpdateData(
                table: "Courencies",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAtUtc",
                value: new DateTime(2025, 11, 7, 21, 1, 1, 838, DateTimeKind.Utc).AddTicks(7465));

            migrationBuilder.UpdateData(
                table: "Courencies",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAtUtc",
                value: new DateTime(2025, 11, 7, 21, 1, 1, 838, DateTimeKind.Utc).AddTicks(7465));

            migrationBuilder.UpdateData(
                table: "Courencies",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAtUtc",
                value: new DateTime(2025, 11, 7, 21, 1, 1, 838, DateTimeKind.Utc).AddTicks(7465));

            migrationBuilder.UpdateData(
                table: "Courencies",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAtUtc",
                value: new DateTime(2025, 11, 7, 21, 1, 1, 838, DateTimeKind.Utc).AddTicks(7465));

            migrationBuilder.UpdateData(
                table: "Courencies",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAtUtc",
                value: new DateTime(2025, 11, 7, 21, 1, 1, 838, DateTimeKind.Utc).AddTicks(7465));

            migrationBuilder.UpdateData(
                table: "Courencies",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedAtUtc",
                value: new DateTime(2025, 11, 7, 21, 1, 1, 838, DateTimeKind.Utc).AddTicks(7465));

            migrationBuilder.UpdateData(
                table: "Courencies",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedAtUtc",
                value: new DateTime(2025, 11, 7, 21, 1, 1, 838, DateTimeKind.Utc).AddTicks(7465));

            migrationBuilder.UpdateData(
                table: "Courencies",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedAtUtc",
                value: new DateTime(2025, 11, 7, 21, 1, 1, 838, DateTimeKind.Utc).AddTicks(7465));

            migrationBuilder.UpdateData(
                table: "Courencies",
                keyColumn: "Id",
                keyValue: 9,
                column: "CreatedAtUtc",
                value: new DateTime(2025, 11, 7, 21, 1, 1, 838, DateTimeKind.Utc).AddTicks(7465));

            migrationBuilder.UpdateData(
                table: "Courencies",
                keyColumn: "Id",
                keyValue: 10,
                column: "CreatedAtUtc",
                value: new DateTime(2025, 11, 7, 21, 1, 1, 838, DateTimeKind.Utc).AddTicks(7465));

            migrationBuilder.UpdateData(
                table: "NotificationTypes",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAtUtc",
                value: new DateTime(2025, 11, 7, 21, 1, 1, 838, DateTimeKind.Utc).AddTicks(7465));

            migrationBuilder.UpdateData(
                table: "NotificationTypes",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAtUtc",
                value: new DateTime(2025, 11, 7, 21, 1, 1, 838, DateTimeKind.Utc).AddTicks(7465));

            migrationBuilder.UpdateData(
                table: "NotificationTypes",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAtUtc",
                value: new DateTime(2025, 11, 7, 21, 1, 1, 838, DateTimeKind.Utc).AddTicks(7465));

            migrationBuilder.UpdateData(
                table: "NotificationTypes",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAtUtc",
                value: new DateTime(2025, 11, 7, 21, 1, 1, 838, DateTimeKind.Utc).AddTicks(7465));

            migrationBuilder.UpdateData(
                table: "OrderStatuses",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAtUtc",
                value: new DateTime(2025, 11, 7, 21, 1, 1, 838, DateTimeKind.Utc).AddTicks(7465));

            migrationBuilder.UpdateData(
                table: "OrderStatuses",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAtUtc",
                value: new DateTime(2025, 11, 7, 21, 1, 1, 838, DateTimeKind.Utc).AddTicks(7465));

            migrationBuilder.UpdateData(
                table: "OrderStatuses",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAtUtc",
                value: new DateTime(2025, 11, 7, 21, 1, 1, 838, DateTimeKind.Utc).AddTicks(7465));

            migrationBuilder.UpdateData(
                table: "OrderStatuses",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAtUtc",
                value: new DateTime(2025, 11, 7, 21, 1, 1, 838, DateTimeKind.Utc).AddTicks(7465));

            migrationBuilder.UpdateData(
                table: "OrderStatuses",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAtUtc",
                value: new DateTime(2025, 11, 7, 21, 1, 1, 838, DateTimeKind.Utc).AddTicks(7465));

            migrationBuilder.UpdateData(
                table: "PaymentMethods",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAtUtc",
                value: new DateTime(2025, 11, 7, 21, 1, 1, 838, DateTimeKind.Utc).AddTicks(7465));

            migrationBuilder.UpdateData(
                table: "PaymentMethods",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAtUtc",
                value: new DateTime(2025, 11, 7, 21, 1, 1, 838, DateTimeKind.Utc).AddTicks(7465));

            migrationBuilder.UpdateData(
                table: "PaymentMethods",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAtUtc",
                value: new DateTime(2025, 11, 7, 21, 1, 1, 838, DateTimeKind.Utc).AddTicks(7465));

            migrationBuilder.UpdateData(
                table: "PaymentMethods",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAtUtc",
                value: new DateTime(2025, 11, 7, 21, 1, 1, 838, DateTimeKind.Utc).AddTicks(7465));

            migrationBuilder.UpdateData(
                table: "PaymentMethods",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAtUtc",
                value: new DateTime(2025, 11, 7, 21, 1, 1, 838, DateTimeKind.Utc).AddTicks(7465));

            migrationBuilder.UpdateData(
                table: "PaymentMethods",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedAtUtc",
                value: new DateTime(2025, 11, 7, 21, 1, 1, 838, DateTimeKind.Utc).AddTicks(7465));

            migrationBuilder.UpdateData(
                table: "PaymentMethods",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedAtUtc",
                value: new DateTime(2025, 11, 7, 21, 1, 1, 838, DateTimeKind.Utc).AddTicks(7465));

            migrationBuilder.UpdateData(
                table: "PaymentMethods",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedAtUtc",
                value: new DateTime(2025, 11, 7, 21, 1, 1, 838, DateTimeKind.Utc).AddTicks(7465));

            migrationBuilder.UpdateData(
                table: "PaymentStatuses",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAtUtc",
                value: new DateTime(2025, 11, 7, 21, 1, 1, 838, DateTimeKind.Utc).AddTicks(7465));

            migrationBuilder.UpdateData(
                table: "PaymentStatuses",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAtUtc",
                value: new DateTime(2025, 11, 7, 21, 1, 1, 838, DateTimeKind.Utc).AddTicks(7465));

            migrationBuilder.UpdateData(
                table: "PaymentStatuses",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAtUtc",
                value: new DateTime(2025, 11, 7, 21, 1, 1, 838, DateTimeKind.Utc).AddTicks(7465));

            migrationBuilder.UpdateData(
                table: "PaymentStatuses",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAtUtc",
                value: new DateTime(2025, 11, 7, 21, 1, 1, 838, DateTimeKind.Utc).AddTicks(7465));

            migrationBuilder.CreateIndex(
                name: "IX_Products_SubcategoryId",
                table: "Products",
                column: "SubcategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Countries_Courencies_CurrencyId",
                table: "Countries",
                column: "CurrencyId",
                principalTable: "Courencies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentTransactions_Courencies_CurrencyId",
                table: "PaymentTransactions",
                column: "CurrencyId",
                principalTable: "Courencies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Subcategories_SubcategoryId",
                table: "Products",
                column: "SubcategoryId",
                principalTable: "Subcategories",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_VariantOptionEntitie_OptionValueEntities_OptionValueId",
                table: "VariantOptionEntitie",
                column: "OptionValueId",
                principalTable: "OptionValueEntities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_VariantOptionEntitie_ProductVariants_VariantId",
                table: "VariantOptionEntitie",
                column: "VariantId",
                principalTable: "ProductVariants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
