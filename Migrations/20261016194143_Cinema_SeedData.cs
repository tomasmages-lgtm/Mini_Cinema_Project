using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mini_Cinema.Migrations
{
    /// <inheritdoc />
    public partial class Cinema_SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("insert into cinemas(Name,Address,Img)values('Lite_VoxCinema','cairo','Cinema_panner.webp')");

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("delete from cinemas where Name='Lite_VoxCinema'");
        }
    }
}
