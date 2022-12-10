using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Northwind.Services.EntityFrameworkCore.Blogging.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BlogArticleProducts",
                columns: table => new
                {
                    BlogArticleProductID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BlogArticleID = table.Column<int>(type: "int", nullable: false),
                    ProductID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlogArticleProducts", x => x.BlogArticleProductID);
                });

            migrationBuilder.CreateTable(
                name: "BlogArticles",
                columns: table => new
                {
                    BlogArticleID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true, defaultValueSql: "((0))"),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValueSql: "((0))"),
                    DatePublished = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PublisherId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlogArticles", x => x.BlogArticleID);
                });

            migrationBuilder.CreateIndex(
                name: "BlogArticleID",
                table: "BlogArticleProducts",
                column: "BlogArticleID");

            migrationBuilder.CreateIndex(
                name: "Title",
                table: "BlogArticles",
                column: "Title");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BlogArticleProducts");

            migrationBuilder.DropTable(
                name: "BlogArticles");
        }
    }
}
