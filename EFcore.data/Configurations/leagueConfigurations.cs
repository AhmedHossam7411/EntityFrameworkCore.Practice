using EFcore.domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFcore.data.Configurations
{
    internal class leagueConfigurations : IEntityTypeConfiguration<league>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<league> builder)
        {
            builder.HasData(
                   new league
                   {
                       Id = 1,           //Primary key is added manually in seeding
                       Name = "Premier League",
                       
                   },

                   new league
                   {
                       Id = 2,           //Primary key is added manually in seeding
                       Name = "la liga",
                       
                   },
                   new league
                   {
                       Id = 3,           //Primary key is added manually in seeding
                       Name = "Egyptian Premier Team",
                       
                   }
                   );  // now i need to add a migration
        }
    }
}
