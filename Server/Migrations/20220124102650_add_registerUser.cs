using Microsoft.EntityFrameworkCore.Migrations;

namespace Server.Migrations
{
    public partial class add_registerUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccountRoles_tb_m_accounts_AccountId",
                table: "AccountRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_AccountRoles_tb_m_roles_RoleId",
                table: "AccountRoles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AccountRoles",
                table: "AccountRoles");

            migrationBuilder.RenameTable(
                name: "AccountRoles",
                newName: "tb_tr_accountrole");

            migrationBuilder.RenameIndex(
                name: "IX_AccountRoles_RoleId",
                table: "tb_tr_accountrole",
                newName: "IX_tb_tr_accountrole_RoleId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_tb_tr_accountrole",
                table: "tb_tr_accountrole",
                columns: new[] { "AccountId", "RoleId" });

            migrationBuilder.AddForeignKey(
                name: "FK_tb_tr_accountrole_tb_m_accounts_AccountId",
                table: "tb_tr_accountrole",
                column: "AccountId",
                principalTable: "tb_m_accounts",
                principalColumn: "NIK",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_tb_tr_accountrole_tb_m_roles_RoleId",
                table: "tb_tr_accountrole",
                column: "RoleId",
                principalTable: "tb_m_roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tb_tr_accountrole_tb_m_accounts_AccountId",
                table: "tb_tr_accountrole");

            migrationBuilder.DropForeignKey(
                name: "FK_tb_tr_accountrole_tb_m_roles_RoleId",
                table: "tb_tr_accountrole");

            migrationBuilder.DropPrimaryKey(
                name: "PK_tb_tr_accountrole",
                table: "tb_tr_accountrole");

            migrationBuilder.RenameTable(
                name: "tb_tr_accountrole",
                newName: "AccountRoles");

            migrationBuilder.RenameIndex(
                name: "IX_tb_tr_accountrole_RoleId",
                table: "AccountRoles",
                newName: "IX_AccountRoles_RoleId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AccountRoles",
                table: "AccountRoles",
                columns: new[] { "AccountId", "RoleId" });

            migrationBuilder.AddForeignKey(
                name: "FK_AccountRoles_tb_m_accounts_AccountId",
                table: "AccountRoles",
                column: "AccountId",
                principalTable: "tb_m_accounts",
                principalColumn: "NIK",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AccountRoles_tb_m_roles_RoleId",
                table: "AccountRoles",
                column: "RoleId",
                principalTable: "tb_m_roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
