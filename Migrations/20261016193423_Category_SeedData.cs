using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mini_Cinema.Migrations
{
    /// <inheritdoc />
    public partial class Category_SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("insert into categories(Name)values" +
                "('action'),('comedy'),('romantic')");

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("delete from categories where Name in ('action','comedy','romantic') ");
        }
    }
}
