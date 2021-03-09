using Microsoft.EntityFrameworkCore.Migrations;

namespace MicroSoft_TcpListener.Migrations
{
    public partial class initall : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CasSalesData_SaleStatus__SaleStatusid",
                table: "CasSalesData");

            migrationBuilder.DropForeignKey(
                name: "FK_CasSalesData_TransactionType__transactionTypeid",
                table: "CasSalesData");

            migrationBuilder.DropTable(
                name: "SaleStatus");

            migrationBuilder.DropTable(
                name: "TransactionType");

            migrationBuilder.DropIndex(
                name: "IX_CasSalesData__SaleStatusid",
                table: "CasSalesData");

            migrationBuilder.DropIndex(
                name: "IX_CasSalesData__transactionTypeid",
                table: "CasSalesData");

            migrationBuilder.DropColumn(
                name: "_SaleStatusid",
                table: "CasSalesData");

            migrationBuilder.DropColumn(
                name: "_transactionTypeid",
                table: "CasSalesData");

            migrationBuilder.AddColumn<bool>(
                name: "Add",
                table: "CasSalesData",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Label",
                table: "CasSalesData",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "NegativeSale",
                table: "CasSalesData",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "NoVoid",
                table: "CasSalesData",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Normal",
                table: "CasSalesData",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Override",
                table: "CasSalesData",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "PluData",
                table: "CasSalesData",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Prepack",
                table: "CasSalesData",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "PrepackData",
                table: "CasSalesData",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Return",
                table: "CasSalesData",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "SelfService",
                table: "CasSalesData",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "TicketData",
                table: "CasSalesData",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Void",
                table: "CasSalesData",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Add",
                table: "CasSalesData");

            migrationBuilder.DropColumn(
                name: "Label",
                table: "CasSalesData");

            migrationBuilder.DropColumn(
                name: "NegativeSale",
                table: "CasSalesData");

            migrationBuilder.DropColumn(
                name: "NoVoid",
                table: "CasSalesData");

            migrationBuilder.DropColumn(
                name: "Normal",
                table: "CasSalesData");

            migrationBuilder.DropColumn(
                name: "Override",
                table: "CasSalesData");

            migrationBuilder.DropColumn(
                name: "PluData",
                table: "CasSalesData");

            migrationBuilder.DropColumn(
                name: "Prepack",
                table: "CasSalesData");

            migrationBuilder.DropColumn(
                name: "PrepackData",
                table: "CasSalesData");

            migrationBuilder.DropColumn(
                name: "Return",
                table: "CasSalesData");

            migrationBuilder.DropColumn(
                name: "SelfService",
                table: "CasSalesData");

            migrationBuilder.DropColumn(
                name: "TicketData",
                table: "CasSalesData");

            migrationBuilder.DropColumn(
                name: "Void",
                table: "CasSalesData");

            migrationBuilder.AddColumn<int>(
                name: "_SaleStatusid",
                table: "CasSalesData",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "_transactionTypeid",
                table: "CasSalesData",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "SaleStatus",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Add = table.Column<bool>(type: "bit", nullable: false),
                    Label = table.Column<bool>(type: "bit", nullable: false),
                    NegativeSale = table.Column<bool>(type: "bit", nullable: false),
                    NoVoid = table.Column<bool>(type: "bit", nullable: false),
                    Override = table.Column<bool>(type: "bit", nullable: false),
                    Prepack = table.Column<bool>(type: "bit", nullable: false),
                    Return = table.Column<bool>(type: "bit", nullable: false),
                    Void = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SaleStatus", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "TransactionType",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Normal = table.Column<bool>(type: "bit", nullable: false),
                    PluData = table.Column<bool>(type: "bit", nullable: false),
                    Prepack = table.Column<bool>(type: "bit", nullable: false),
                    SelfService = table.Column<bool>(type: "bit", nullable: false),
                    TicketData = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransactionType", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CasSalesData__SaleStatusid",
                table: "CasSalesData",
                column: "_SaleStatusid");

            migrationBuilder.CreateIndex(
                name: "IX_CasSalesData__transactionTypeid",
                table: "CasSalesData",
                column: "_transactionTypeid");

            migrationBuilder.AddForeignKey(
                name: "FK_CasSalesData_SaleStatus__SaleStatusid",
                table: "CasSalesData",
                column: "_SaleStatusid",
                principalTable: "SaleStatus",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CasSalesData_TransactionType__transactionTypeid",
                table: "CasSalesData",
                column: "_transactionTypeid",
                principalTable: "TransactionType",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
