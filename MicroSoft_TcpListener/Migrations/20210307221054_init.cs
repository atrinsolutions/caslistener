using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MicroSoft_TcpListener.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SaleStatus",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NegativeSale = table.Column<bool>(type: "bit", nullable: false),
                    Return = table.Column<bool>(type: "bit", nullable: false),
                    Void = table.Column<bool>(type: "bit", nullable: false),
                    Prepack = table.Column<bool>(type: "bit", nullable: false),
                    Label = table.Column<bool>(type: "bit", nullable: false),
                    Override = table.Column<bool>(type: "bit", nullable: false),
                    Add = table.Column<bool>(type: "bit", nullable: false),
                    NoVoid = table.Column<bool>(type: "bit", nullable: false)
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
                    Prepack = table.Column<bool>(type: "bit", nullable: false),
                    SelfService = table.Column<bool>(type: "bit", nullable: false),
                    PluData = table.Column<bool>(type: "bit", nullable: false),
                    TicketData = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransactionType", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "CasSalesData",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CasProtocolIdentifier = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FunctionCode = table.Column<byte>(type: "tinyint", nullable: false),
                    PackLenght = table.Column<long>(type: "bigint", nullable: false),
                    ScaleLocked = table.Column<byte>(type: "tinyint", nullable: false),
                    ScaleIP = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ScalePort = table.Column<long>(type: "bigint", nullable: false),
                    ScaleServiceType = table.Column<byte>(type: "tinyint", nullable: false),
                    ScaleTail = table.Column<long>(type: "bigint", nullable: false),
                    _transactionTypeid = table.Column<int>(type: "int", nullable: true),
                    ScaleID = table.Column<byte>(type: "tinyint", nullable: false),
                    PluType = table.Column<byte>(type: "tinyint", nullable: false),
                    DeparmentNumber = table.Column<byte>(type: "tinyint", nullable: false),
                    PLUNumber = table.Column<long>(type: "bigint", nullable: false),
                    Itemcode = table.Column<long>(type: "bigint", nullable: false),
                    Weight = table.Column<long>(type: "bigint", nullable: false),
                    Qty = table.Column<short>(type: "smallint", nullable: false),
                    Pcs = table.Column<short>(type: "smallint", nullable: false),
                    UnitPrice = table.Column<long>(type: "bigint", nullable: false),
                    TotalPrice = table.Column<long>(type: "bigint", nullable: false),
                    DiscountPrice = table.Column<long>(type: "bigint", nullable: false),
                    ScaleTransactioncounter = table.Column<long>(type: "bigint", nullable: false),
                    TicketNumber = table.Column<short>(type: "smallint", nullable: false),
                    _SaleStatusid = table.Column<int>(type: "int", nullable: true),
                    CurrentDate_year = table.Column<long>(type: "bigint", nullable: false),
                    CurrentDate_month = table.Column<long>(type: "bigint", nullable: false),
                    CurrentDate_day = table.Column<long>(type: "bigint", nullable: false),
                    CurrentTime_hour = table.Column<long>(type: "bigint", nullable: false),
                    CurrentTime_min = table.Column<long>(type: "bigint", nullable: false),
                    CurrentTime_second = table.Column<long>(type: "bigint", nullable: false),
                    SaleDate_year = table.Column<long>(type: "bigint", nullable: false),
                    SaleDate_month = table.Column<long>(type: "bigint", nullable: false),
                    SaleDate_day = table.Column<long>(type: "bigint", nullable: false),
                    Barcode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TraceCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CurrentTicketSaleorder = table.Column<byte>(type: "tinyint", nullable: false),
                    reserved = table.Column<byte>(type: "tinyint", nullable: false),
                    PluName = table.Column<byte[]>(type: "varbinary(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CasSalesData", x => x.id);
                    table.ForeignKey(
                        name: "FK_CasSalesData_SaleStatus__SaleStatusid",
                        column: x => x._SaleStatusid,
                        principalTable: "SaleStatus",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CasSalesData_TransactionType__transactionTypeid",
                        column: x => x._transactionTypeid,
                        principalTable: "TransactionType",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CasSalesData__SaleStatusid",
                table: "CasSalesData",
                column: "_SaleStatusid");

            migrationBuilder.CreateIndex(
                name: "IX_CasSalesData__transactionTypeid",
                table: "CasSalesData",
                column: "_transactionTypeid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CasSalesData");

            migrationBuilder.DropTable(
                name: "SaleStatus");

            migrationBuilder.DropTable(
                name: "TransactionType");
        }
    }
}
