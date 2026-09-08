using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ECommerce.Infrastructure.Migrations.Application
{
    /// <inheritdoc />
    public partial class AddAddressFlagsAndLabel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsDefault",
                table: "UserAddresses",
                newName: "IsDefaultShipping");

            migrationBuilder.AddColumn<bool>(
                name: "IsDefaultBilling",
                table: "UserAddresses",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Label",
                table: "UserAddresses",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDefaultBilling",
                table: "UserAddresses");

            migrationBuilder.DropColumn(
                name: "Label",
                table: "UserAddresses");

            migrationBuilder.RenameColumn(
                name: "IsDefaultShipping",
                table: "UserAddresses",
                newName: "IsDefault");
        }
    }
}
