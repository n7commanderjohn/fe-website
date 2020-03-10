using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FEWebsite.API.Migrations
{
    public partial class PopulateGameGenresTable : MigrationExtensions
    {
        private const string Table = "GameGenres";

        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var valuesToInsert = new List<string>() {
                "'AC', 'Action'", "'AD', 'Adventure'", "'RPG', 'Role Playing Game'", "'S', 'Strategy'"
            };
            this.Insert(migrationBuilder, Table + " (Name, Description)", valuesToInsert);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            this.Delete(migrationBuilder, Table);
        }
    }
}
