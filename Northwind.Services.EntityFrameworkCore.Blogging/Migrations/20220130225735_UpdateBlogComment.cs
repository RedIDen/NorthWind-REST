using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Northwind.Services.EntityFrameworkCore.Blogging.Migrations
{
    public partial class UpdateBlogComment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PublisherID",
                table: "BlogComments",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PublisherID",
                table: "BlogComments");
        }
    }
}
