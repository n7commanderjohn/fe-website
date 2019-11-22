using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FEWebsite.API.Migrations
{
    public partial class PopulateGamesTable : MigrationExtensions
    {
        private const string Table = "Games (Name, Description)";

        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var valuesToInsert = new List<string>();
            for (var num = 1; num < 17; num++) {
                valuesToInsert.Add($"'FE{num}', 'Fire Emblem {num}'");
            }
            this.Insert(migrationBuilder, Table, valuesToInsert);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            this.Delete(migrationBuilder, "Games");
        }
    }
}
