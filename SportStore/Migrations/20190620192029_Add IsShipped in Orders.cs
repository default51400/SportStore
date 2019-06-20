﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace SportStore.Migrations
{
    public partial class AddIsShippedinOrders : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsShipped",
                table: "Orders",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsShipped",
                table: "Orders");
        }
    }
}
