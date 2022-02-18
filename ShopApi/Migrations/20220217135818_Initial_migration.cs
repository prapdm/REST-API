using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ShopApi.Migrations
{
    public partial class Initial_migration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Addresses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    City = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Street = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    PostalCode = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    Surname = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Shops",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HasDelivery = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    ContactEmail = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(9)", maxLength: 9, nullable: true),
                    Website = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Category = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    AddressId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shops", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Shops_Addresses_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Addresses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Shops_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(10,4)", nullable: false),
                    ShopId = table.Column<int>(type: "int", nullable: false),
                    Category = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Shops_ShopId",
                        column: x => x.ShopId,
                        principalTable: "Shops",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_ShopId",
                table: "Products",
                column: "ShopId");

            migrationBuilder.CreateIndex(
                name: "IX_Shops_AddressId",
                table: "Shops",
                column: "AddressId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Shops_CreatedById",
                table: "Shops",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleId",
                table: "Users",
                column: "RoleId");

            migrationBuilder.Sql(
@"CREATE TRIGGER [dbo].[Users_UPDATE] ON [dbo].[Users]
                AFTER UPDATE
            AS
            BEGIN
                SET NOCOUNT ON;

                IF ((SELECT TRIGGER_NESTLEVEL()) > 1) RETURN;

                DECLARE @Id INT

                SELECT @Id = INSERTED.Id
                FROM INSERTED

                UPDATE dbo.Users
                SET UpdatedAt = GETDATE()
                WHERE Id = @Id
            END");

            migrationBuilder.Sql(
            @"CREATE TRIGGER [dbo].[Roles_UPDATE] ON [dbo].[Roles]
                AFTER UPDATE
            AS
            BEGIN
                SET NOCOUNT ON;

                IF ((SELECT TRIGGER_NESTLEVEL()) > 1) RETURN;

                DECLARE @Id INT

                SELECT @Id = INSERTED.Id
                FROM INSERTED

                UPDATE dbo.Roles
                SET UpdatedAt = GETDATE()
                WHERE Id = @Id
            END");

            migrationBuilder.Sql(
            @"CREATE TRIGGER [dbo].[Products_UPDATE] ON [dbo].[Products]
                AFTER UPDATE
            AS
            BEGIN
                SET NOCOUNT ON;

                IF ((SELECT TRIGGER_NESTLEVEL()) > 1) RETURN;

                DECLARE @Id INT

                SELECT @Id = INSERTED.Id
                FROM INSERTED

                UPDATE dbo.Products
                SET UpdatedAt = GETDATE()
                WHERE Id = @Id
            END");


            migrationBuilder.Sql(
            @"CREATE TRIGGER [dbo].[Addresses_UPDATE] ON [dbo].[Addresses]
                AFTER UPDATE
            AS
            BEGIN
                SET NOCOUNT ON;

                IF ((SELECT TRIGGER_NESTLEVEL()) > 1) RETURN;

                DECLARE @Id INT

                SELECT @Id = INSERTED.Id
                FROM INSERTED

                UPDATE dbo.Addresses
                SET UpdatedAt = GETDATE()
                WHERE Id = @Id
            END");

            migrationBuilder.Sql(
            @"CREATE TRIGGER [dbo].[Shops_UPDATE] ON [dbo].[Shops]
                AFTER UPDATE
            AS
            BEGIN
                SET NOCOUNT ON;

                IF ((SELECT TRIGGER_NESTLEVEL()) > 1) RETURN;

                DECLARE @Id INT

                SELECT @Id = INSERTED.Id
                FROM INSERTED

                UPDATE dbo.Shops
                SET UpdatedAt = GETDATE()
                WHERE Id = @Id
            END");


        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Shops");

            migrationBuilder.DropTable(
                name: "Addresses");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}
