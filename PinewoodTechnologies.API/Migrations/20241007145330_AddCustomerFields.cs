﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PinewoodTechnologies.API.Migrations
{
    /// <inheritdoc />
    public partial class AddCustomerFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FirstName = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    DoB = table.Column<DateTime>(type: "TEXT", nullable: false),
                    AddressLine1 = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    AddressLine2 = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    AddressLine3 = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Postcode = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    TelephoneNumber = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Customers");
        }
    }
}
