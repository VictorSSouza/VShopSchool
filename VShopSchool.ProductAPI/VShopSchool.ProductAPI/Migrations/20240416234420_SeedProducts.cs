using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VShopSchool.ProductAPI.Migrations
{
    /// <inheritdoc />
    public partial class SeedProducts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder mb)
        {
            mb.Sql("Insert into Products(Name, Price, Description, Stock, ImageUrl, CategoryId)" +
                    "Values('Caderno Espiral', 19.99, 'Caderno Espiral 1 Matéria', 100,'cadernoespi_1.png', 1)");

            mb.Sql("Insert into Products(Name, Price, Description, Stock, ImageUrl, CategoryId)" +
                    "Values('Caderno Brochura', 17.99, 'Caderno Brochura 80 folhas', 80,'cadernobrochu_1.png', 1)");

            mb.Sql("Insert into Products(Name, Price, Description, Stock, ImageUrl, CategoryId)" +
                    "Values('Caderno Smart Colegial', 9.99, 'Caderno Smart Colegial 80 folhas', 70,'cadernosmacol_1.png', 1)");

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder mb)
        {
            mb.Sql("Delete from Products");
        }
    }
}
