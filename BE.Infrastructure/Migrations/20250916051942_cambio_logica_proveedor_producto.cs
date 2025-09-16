using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BE.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class cambio_logica_proveedor_producto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "product_price");

            migrationBuilder.DropTable(
                name: "providers");

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "products",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateTable(
                name: "suppliers",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    es_activo = table.Column<bool>(type: "bit", nullable: false),
                    creado = table.Column<DateTime>(type: "datetime2", nullable: false),
                    actualizado = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_suppliers", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "supplier_products",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    product_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    supplier_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    stock = table.Column<int>(type: "int", nullable: false),
                    lot_number = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    es_activo = table.Column<bool>(type: "bit", nullable: false),
                    creado = table.Column<DateTime>(type: "datetime2", nullable: false),
                    actualizado = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_supplier_products", x => x.id);
                    table.ForeignKey(
                        name: "fk_supplier_products_products_product_id",
                        column: x => x.product_id,
                        principalTable: "products",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_supplier_products_suppliers_supplier_id",
                        column: x => x.supplier_id,
                        principalTable: "suppliers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_supplier_products_product_id_supplier_id_lot_number",
                table: "supplier_products",
                columns: new[] { "product_id", "supplier_id", "lot_number" });

            migrationBuilder.CreateIndex(
                name: "ix_supplier_products_supplier_id",
                table: "supplier_products",
                column: "supplier_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "supplier_products");

            migrationBuilder.DropTable(
                name: "suppliers");

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "products",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.CreateTable(
                name: "product_price",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    lot_number = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    product_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    provider_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    stock = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_product_price", x => x.id);
                    table.ForeignKey(
                        name: "fk_product_price_products_product_id",
                        column: x => x.product_id,
                        principalTable: "products",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "providers",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_providers", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "ix_product_price_product_id",
                table: "product_price",
                column: "product_id");
        }
    }
}
