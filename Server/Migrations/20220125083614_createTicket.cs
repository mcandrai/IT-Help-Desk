using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Server.Migrations
{
    public partial class createTicket : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tb_m_categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_m_categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tb_m_statuses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_m_statuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tb_m_tickets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreateAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    StatusId = table.Column<int>(type: "int", nullable: false),
                    NIK = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_m_tickets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tb_m_tickets_tb_m_categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "tb_m_categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tb_m_tickets_tb_m_employees_NIK",
                        column: x => x.NIK,
                        principalTable: "tb_m_employees",
                        principalColumn: "NIK",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tb_m_tickets_tb_m_statuses_StatusId",
                        column: x => x.StatusId,
                        principalTable: "tb_m_statuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tb_tr_messages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    MessageText = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TicketId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_tr_messages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tb_tr_messages_tb_m_tickets_Id",
                        column: x => x.Id,
                        principalTable: "tb_m_tickets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tb_m_tickets_CategoryId",
                table: "tb_m_tickets",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_tb_m_tickets_NIK",
                table: "tb_m_tickets",
                column: "NIK");

            migrationBuilder.CreateIndex(
                name: "IX_tb_m_tickets_StatusId",
                table: "tb_m_tickets",
                column: "StatusId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tb_tr_messages");

            migrationBuilder.DropTable(
                name: "tb_m_tickets");

            migrationBuilder.DropTable(
                name: "tb_m_categories");

            migrationBuilder.DropTable(
                name: "tb_m_statuses");
        }
    }
}
