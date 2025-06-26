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
using EFcore.data.Configurations;
using EFcore.domain;


namespace EFcore.data
{

    public class FootballLeagueDbContext : DbContext  // code embodiment of db
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
        public DbSet<Match> Matches { get; set; }
        public DbSet<league> leagues { get; set; }

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
            modelBuilder.ApplyConfiguration(new TeamConfigurations());         // Apply the TeamConfigurations class to configure the Team entity
            modelBuilder.ApplyConfiguration(new leagueConfigurations());  // Apply the leagueConfigurations class to configure the Team entity

        }
    }
}
