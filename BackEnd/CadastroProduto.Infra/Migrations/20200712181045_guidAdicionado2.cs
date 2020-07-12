using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CadastroProduto.Infra.Migrations
{
    public partial class guidAdicionado2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "Guid",
                table: "Produto",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Guid",
                table: "Produto");
        }
    }
}
