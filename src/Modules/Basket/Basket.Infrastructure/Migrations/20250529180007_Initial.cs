﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Basket.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "basket");

            migrationBuilder.CreateTable(
                name: "shopping_carts",
                schema: "basket",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_shopping_carts", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "shopping_cart_items",
                schema: "basket",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    shopping_cart_id = table.Column<Guid>(type: "uuid", nullable: false),
                    product_id = table.Column<Guid>(type: "uuid", nullable: false),
                    quantity = table.Column<int>(type: "integer", nullable: false),
                    color = table.Column<string>(type: "text", nullable: false),
                    price = table.Column<decimal>(type: "numeric", nullable: false),
                    product_name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_shopping_cart_items", x => x.id);
                    table.ForeignKey(
                        name: "fk_shopping_cart_items_shopping_carts_shopping_cart_id",
                        column: x => x.shopping_cart_id,
                        principalSchema: "basket",
                        principalTable: "shopping_carts",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_shopping_cart_items_shopping_cart_id",
                schema: "basket",
                table: "shopping_cart_items",
                column: "shopping_cart_id");

            migrationBuilder.CreateIndex(
                name: "ix_shopping_carts_user_name",
                schema: "basket",
                table: "shopping_carts",
                column: "user_name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "shopping_cart_items",
                schema: "basket");

            migrationBuilder.DropTable(
                name: "shopping_carts",
                schema: "basket");
        }
    }
}
