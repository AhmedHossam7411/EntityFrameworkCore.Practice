using EFcore.data;
using EFcore.domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using System.Collections.Frozen;
using System.Collections.Generic;


FootballLeagueDbContext context = new FootballLeagueDbContext();
  


 async Task listing()
    {
        var teams = context.Teams.ToList();

        foreach (var item in teams)
        {
            Console.WriteLine(item.Name);
        }
        var firstTeam = await context.Teams.Where
        (team => team.TeamId > 1).ToListAsync();

        Console.WriteLine(firstTeam);
    }
     async Task aggregateFunctions()
    {
        var numberTeams = await context.Teams.CountAsync(); // count
        var numberTeamsCondition = await context.Teams.CountAsync(team => team.Name.Contains("l"));  // count with condition
        var numberMax = await context.Teams.MaxAsync(team => team.Name.Contains("l")); // max number
        var numberMin = await context.Teams.MinAsync(team => team.CreatedDate); // min number for condition
        var numberAvg = await context.Teams.AverageAsync(team => team.TeamId); // avg
        var numberSum = await context.Teams.SumAsync(team => team.TeamId); // sum
    }

     async Task grouping()
    {
        // group by

        var groupedTeams = context.Teams.Where(q => q.Name.Contains($"l"))
            .GroupBy(team => team.CreatedDate);// Filter first then , group on created date

        foreach (var group in groupedTeams)
        {
            Console.WriteLine(group.Key);
            // Console.WriteLine(group);
        } // the grouping key  
    }
     async Task ordering()
    {
        // Ordering :

        var orderedTeams = context.Teams.OrderByDescending(o => o.TeamId);
        foreach (var team in orderedTeams) Console.WriteLine(team.Name);

        //var maxby = context.Teams.MaxBy(q => q.TeamId);
        //Console.WriteLine(maxby.Name);           // crashes because EF core cant convert this to sql , can use AsEnumerable OR use OrderBy 

        // insert into teams
    }
     async Task skipAndTake()
    {
        // skip and take , useful for paging 
        var recordPerPage = 3;
        var Page = 0;
        var nextButton = true;
        while (nextButton)
        {
            var teamer = await context.Teams.Skip(Page * recordPerPage)
                .Take(recordPerPage).ToListAsync();
            foreach (var team in teamer) Console.WriteLine(team.Name);
            nextButton = Convert.ToBoolean(Console.ReadLine());
            if (!nextButton) break;
            Page += 1;
        }
    }

    /*var allTeams = await context.Teams.ToListAsync();
    foreach (var t in allTeams)
        Console.WriteLine($"{t.TeamId} - {t.Name}");*/

     async Task selector()
    {
        var selector = await context.Teams.Select(team => team.Name).ToListAsync();// select only name and teamId
        foreach (var team in selector)
        {
            Console.WriteLine($"{team}");
        }
    }
async Task inserting()
{
    //inserting

    var newCoach = new Coach
    {
        Name = "Zinedine Zidane",
        CreatedDate = DateTime.Now
    };
    await context.Coaches.AddAsync(newCoach);
    await context.SaveChangesAsync();

    //Loop insert
    List<Coach> listInsert = new List<Coach>
{
    new Coach { Name = "Pep Guardiola", CreatedDate = DateTime.Now },
    new Coach { Name = "Jurgen Klopp", CreatedDate = DateTime.Now },
    new Coach { Name = "Antonio Conte", CreatedDate = DateTime.Now }
};
    foreach (var item in listInsert)
    {
        await context.Coaches.AddAsync(item);
    }
    await context.SaveChangesAsync();

    //batch insert
await context.Coaches.AddRangeAsync(listInsert);
    await context.SaveChangesAsync();

}








