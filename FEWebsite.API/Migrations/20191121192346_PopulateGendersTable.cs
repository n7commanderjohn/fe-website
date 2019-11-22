using Microsoft.EntityFrameworkCore.Migrations;

namespace FEWebsite.API.Migrations
{
    public partial class PopulateGendersTable : MigrationExtensions
    {
        private const string Table = "Genders";

        protected override void Up(MigrationBuilder migrationBuilder)
        {
            this.Insert(migrationBuilder, Table, "'M', 'Male'");
            this.Insert(migrationBuilder, Table, "'F', 'Female'");
            this.Insert(migrationBuilder, Table, "'NA', 'Prefer not to say'");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            this.Delete(migrationBuilder, Table);
        }
    }
}
