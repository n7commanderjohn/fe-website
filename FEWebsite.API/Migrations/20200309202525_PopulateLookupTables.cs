using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FEWebsite.API.Migrations
{
    public partial class PopulateLookupTables : MigrationExtensions
    {
        private const string Genders = "Genders";
        private const string Games = "Games (Name, Description)";
        private const string Genres = "GameGenres";

        protected override void Up(MigrationBuilder migrationBuilder)
        {
#region Genders
            this.Insert(migrationBuilder, Genders, "'M', 'Male'");
            this.Insert(migrationBuilder, Genders, "'F', 'Female'");
            this.Insert(migrationBuilder, Genders, "'NA', 'Prefer not to say'");
#endregion

#region Games
            var gamesToInsert = new List<string>();
            for (var num = 1; num < 17; num++) {
                gamesToInsert.Add($"'FE{num}', 'Fire Emblem {num}'");
            }
            this.Insert(migrationBuilder, Games, gamesToInsert);
#endregion

#region Genres
            var genresToInsert = new List<string>() {
                "'AC', 'Action'", "'AD', 'Adventure'", "'RPG', 'Role Playing Game'", "'S', 'Strategy'"
            };
            this.Insert(migrationBuilder, Genres + " (Name, Description)", genresToInsert);
#endregion
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            this.Delete(migrationBuilder, Genders);
            this.Delete(migrationBuilder, "Games");
            this.Delete(migrationBuilder, Genres);
        }
    }
}
