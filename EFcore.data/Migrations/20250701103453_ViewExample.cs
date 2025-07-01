using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EFcore.data.Migrations
{
    /// <inheritdoc />
    public partial class ViewExample : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                CREATE VIEW LeagueDetails AS
                SELECT l.Id, l.Name, COUNT(t.Id) AS TeamCount
                FROM leagues l
                LEFT JOIN teams t ON l.Id = t.LeagueId
                GROUP BY l.Id, l.Name;
        
            ");
        }


        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                DROP VIEW IF EXISTS LeagueDetails;
         ");
        }
    }
}
    

