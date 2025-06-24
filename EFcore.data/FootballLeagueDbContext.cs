using EFcore.domain; // domain project
using Microsoft.EntityFrameworkCore;  //EF core package
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Identity.Client;
using LogLevel = Microsoft.Extensions.Logging.LogLevel;


namespace EFcore.data
{
    
    public  class FootballLeagueDbContext : DbContext  // code embodiment of db
    {
        

        public FootballLeagueDbContext()
        {
            string baseDir = AppDomain.CurrentDomain.BaseDirectory;
            string solutionDir = Directory.GetParent(baseDir).Parent.Parent.Parent.FullName;

            dbPath = Path.Combine(solutionDir, "FootballLeague_EFCore.db");

            Console.WriteLine(" this is the path " + dbPath);
        }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Coach> Coaches { get; set; }

        public string dbPath { get; set; }

        //start by typing override
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Data Source={dbPath}")
                .LogTo(Console.WriteLine, LogLevel.Information)
             .EnableSensitiveDataLogging()
                 .EnableDetailedErrors();       //only for learning 
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Team>().HasData(
                new Team
                {
                    TeamId = 1,           //Primary key is added manually in seeding
                    Name = "Fiorentina",
                    CreatedDate = new DateTime(2025, 06, 19)
                },

                new Team
                {
                    TeamId = 2,           //Primary key is added manually in seeding
                    Name = "Realmadrid",
                    CreatedDate = new DateTime(2025, 06, 19)
                },
                new Team
                {
                    TeamId = 3,           //Primary key is added manually in seeding
                    Name = "Liverpool",
                    CreatedDate = new DateTime(2025, 06, 19)
                }
                );  // now i need to add a migration
        }
    }
}
