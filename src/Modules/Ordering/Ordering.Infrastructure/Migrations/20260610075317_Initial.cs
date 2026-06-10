using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ordering.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "orders",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    customer_id = table.Column<Guid>(type: "uuid", nullable: false),
                    order_name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    billing_address_address_line = table.Column<string>(type: "character varying(180)", maxLength: 180, nullable: false),
                    billing_address_country = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    billing_address_email_address = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    billing_address_first_name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    billing_address_last_name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    billing_address_state = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    billing_address_zip_code = table.Column<string>(type: "character varying(5)", maxLength: 5, nullable: false),
                    payment_cvv = table.Column<string>(type: "character varying(3)", maxLength: 3, nullable: false),
                    payment_card_name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    payment_card_number = table.Column<string>(type: "character varying(24)", maxLength: 24, nullable: false),
                    payment_expiration = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    payment_payment_method = table.Column<int>(type: "integer", nullable: false),
                    shipping_address_address_line = table.Column<string>(type: "character varying(180)", maxLength: 180, nullable: false),
                    shipping_address_country = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    shipping_address_email_address = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    shipping_address_first_name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    shipping_address_last_name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    shipping_address_state = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    shipping_address_zip_code = table.Column<string>(type: "character varying(5)", maxLength: 5, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_orders", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "outbox_messages",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    data = table.Column<string>(type: "text", nullable: false),
                    type = table.Column<string>(type: "text", nullable: false),
                    event_id = table.Column<Guid>(type: "uuid", nullable: false),
                    event_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    state = table.Column<int>(type: "integer", nullable: false),
                    modified_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    content = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_outbox_messages", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "order_items",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    order_id = table.Column<Guid>(type: "uuid", nullable: false),
                    product_id = table.Column<Guid>(type: "uuid", nullable: false),
                    quantity = table.Column<int>(type: "integer", nullable: false),
                    price = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_order_items", x => x.id);
                    table.ForeignKey(
                        name: "fk_order_items_orders_order_id",
                        column: x => x.order_id,
                        principalTable: "orders",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_order_items_order_id",
                table: "order_items",
                column: "order_id");

            migrationBuilder.CreateIndex(
                name: "ix_orders_order_name",
                table: "orders",
                column: "order_name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "order_items");

            migrationBuilder.DropTable(
                name: "outbox_messages");

            migrationBuilder.DropTable(
                name: "orders");
        }
    }
}
