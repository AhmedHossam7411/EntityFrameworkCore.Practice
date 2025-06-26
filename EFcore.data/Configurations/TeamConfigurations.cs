// Configure the Team entity here, e.g.:
// builder.HasKey(t => t.Id);
// builder.Property(t => t.Name).IsRequired().HasMaxLength(100);
// Add other configurations as needed
using EFcore.domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace EFcore.data.Configurations
{
    internal class TeamConfigurations : IEntityTypeConfiguration<Team>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Team> builder)
        {
            builder.HasIndex(q => q.Name).IsUnique(); // this will create a unique index on the Name column
            builder.HasMany(q => q.HomeMatches)
                   .WithOne(q => q.HomeTeam)
                   .HasForeignKey(q => q.HomeTeamId)
                   .IsRequired()
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(q => q.AwayMatches)
                   .WithOne(q => q.AwayTeam)
                   .HasForeignKey(q => q.AwayTeamId)
                   .IsRequired()
                   .OnDelete(DeleteBehavior.Restrict);
            // this will create a foreign key relationship between the Team and Match entities 
            builder.HasOne(t => t.Coach)
               .WithOne(c => c.Team)
               .HasForeignKey<Team>(t => t.CoachId)
               .IsRequired(false); 
                
            builder.HasData(
                   new Team
                   {
                       Id = 1,           //Primary key is added manually in seeding
                       Name = "Fiorentina",
                       CreatedDate = new DateTime(2025, 06, 19)
                   },

                   new Team
                   {
                       Id = 2,           //Primary key is added manually in seeding
                       Name = "Realmadrid",
                       CreatedDate = new DateTime(2025, 06, 19)
                   },
                   new Team
                   {
                       Id = 3,           //Primary key is added manually in seeding
                       Name = "Liverpool",
                       CreatedDate = new DateTime(2025, 06, 19)
                   }
                   );  // now i need to add a migration
        }
    }

}
    

