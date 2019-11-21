using System.Text;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FEWebsite.API.Migrations
{
    public abstract class MigrationExtensions : Migration
    {
        #region Public Methods

        /// <summary>
        /// Format = "DELETE FROM {table};" // used if argWhere is not provided
        /// Format = "DELETE FROM {table} WHERE ({argWhere});"
        /// </summary>
        /// <param name="table">sql table</param>
        /// <param name="argWhere">arguments to provide as where clause</param>
        protected void Delete(MigrationBuilder migrationBuilder, string table, string argWhere = null)
        {
            string sql;
            if (string.IsNullOrEmpty(argWhere)) {
                sql = string.Format("DELETE FROM {0};",
                    table
                );
            } else {
                sql = string.Format("DELETE FROM {0} WHERE ({1});",
                    table, argWhere
                );
            }

            migrationBuilder.Sql(sql);
        }

        /// <summary>
        /// Format = "INSERT INTO {table} VALUES ({argValues});"
        /// </summary>
        /// <param name="table">sql table</param>
        /// <param name="argValues">arguments to provide as values</param>
        protected void Insert(MigrationBuilder migrationBuilder, string table, string argValues)
        {
            var sql = string.Format("INSERT INTO {0} VALUES ({1});",
                table, argValues
            );

            migrationBuilder.Sql(sql);
        }

        /// <summary>
        /// Format = "INSERT INTO {table} VALUES ({argValues});"
        /// </summary>
        /// <param name="table">sql table</param>
        /// <param name="argValues">provide a list of arguments to provide as values</param>
        protected void Insert(MigrationBuilder migrationBuilder, string table, List<string> argValues)
        {
            var valuesToInsert = new StringBuilder();
            foreach (var arg in argValues) {
                var append = $"({arg}), ";
                valuesToInsert.Append(append);
            }

            var valuesString = valuesToInsert.ToString();
            valuesString = valuesString.Substring(0, valuesString.LastIndexOf(','));
            var sql = string.Format("INSERT INTO {0} VALUES {1};",
                table, valuesString
            );

            migrationBuilder.Sql(sql);
        }

        /// <summary>
        /// Format = "UPDATE {table} SET {argSetClause};"
        /// </summary>
        /// <param name="table">sql table</param>
        /// <param name="argSetClause">arguments to provide as set clause</param>
        protected void Update(MigrationBuilder migrationBuilder, string table, string argSetClause)
        {
            var sql = string.Format("UPDATE {0} SET {1};",
                table, argSetClause
            );

            migrationBuilder.Sql(sql);
        }

        protected void DropFK(MigrationBuilder migrationBuilder, string fkName, string alteredTable)
        {
            migrationBuilder.Sql(
                string.Format(@"
                    IF EXISTS (
                        SELECT * FROM sys.objects
                        WHERE name = N'{0}' AND [type] = 'F'
                    )
                    ALTER TABLE [{1}]
                    DROP Constraint [{2}]
                ",
                fkName, alteredTable, fkName)
            );
        }

        protected void MakeFK(MigrationBuilder migrationBuilder, string fkName, string alteredTable, string alteredTableFKColumnName, string parentTable, string parentColForFK, string deleteAndUpdateActions)
        {
            migrationBuilder.Sql(
                string.Format(@"
                    ALTER TABLE [{0}]
                    ADD CONSTRAINT [{1}]
                    FOREIGN KEY({2}) REFERENCES {3}({4})
                    {5}
                ",
                alteredTable, fkName, alteredTableFKColumnName, parentTable,
                parentColForFK, deleteAndUpdateActions)
            );
        }
    }

    #endregion Public Methods
}
