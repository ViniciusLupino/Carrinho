using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PF.Data.Migrations
{
    /// <inheritdoc />
    public partial class StatusId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StatusId",
                table: "StatusPedido",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StatusId",
                table: "StatusPedido");
        }
    }
}
