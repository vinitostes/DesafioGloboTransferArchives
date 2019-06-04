using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DesafioGlobo.Infra.Data.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TransferArchiveControl",
                columns: table => new
                {
                    IdTransferArchiveControl = table.Column<Guid>(nullable: false),
                    TypeAction = table.Column<int>(nullable: false),
                    Request = table.Column<string>(type: "varchar(max)", nullable: false),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    IdResponse = table.Column<Guid>(nullable: true),
                    CheckSum = table.Column<string>(type: "varchar(150)", nullable: true),
                    DateSend = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransferArchiveControl", x => x.IdTransferArchiveControl);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TransferArchiveControl");
        }
    }
}
