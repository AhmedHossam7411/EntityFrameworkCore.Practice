using Azure;
using EFcore.data;
using EFcore.domain;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using System.Collections.Frozen;
using System.Collections.Generic;
using System.Linq;


FootballLeagueDbContext context = new FootballLeagueDbContext();

void OtherRawQueries()
{
    /* Executing Stored Procedures
    var leagueId = 1;
    var league = context.Leagues
        .FromSqlInterpolated($"EXEC dbo.StoredProcedureToGetLeagueNameHere {leagueId}");*/

    // Non-querying statement 
    var someName = "Random Team Name";
    context.Database.ExecuteSqlInterpolated($"UPDATE Teams SET Name = {someName}");

    int matchId = 1;
    context.Database.ExecuteSqlInterpolated($"EXEC dbo.DeleteMatch {matchId}");

    // Query Scalar or Non-Entity Type
    var leagueIds = context.Database.SqlQuery<int>($"SELECT Id FROM Leagues")
        .ToList();

    // Execute User-Defined Query
    //var earliestMatch = context.GetEarliestTeamMatch(1);
}
async Task sqlSyntax()
{
    Console.WriteLine("enter team name");
    var teamName = Console.ReadLine();
    var teamNameParam = new SqliteParameter("TeamName", teamName);
    var teams = context.Teams.FromSqlRaw($"select * from Teams where Name = @TeamName", teamNameParam);
    foreach (var team in teams)
    {
        Console.WriteLine(team.Name);
    }

     // FROM SQL
  teams = context.Teams.FromSql($"SELECT * FROM Teams WHERE name = {teamName}");
    foreach (var t in teams)
    {
        Console.WriteLine(t);
    }

     // FROM SQL INTERPOLATED

     teams = context.Teams.FromSqlInterpolated($"SELECT * FROM Teams WHERE name = {teamName}");
    foreach (var t in teams)
    {
        Console.WriteLine(t);
    }
}
async Task queryKeylessEntity() { 
var viewer = await context.TeamsAndLeaguesView.ToListAsync(); 
foreach (var item in viewer)
{
    Console.WriteLine($"Team Name: {item.Name} ");
}

}
async Task filteringInclude()
{
var filterer = context.leagues.Include(q => q.Teams).Where(q => q.Name.Contains("A")).ToListAsync();
foreach (var item in filterer.Result)
{
    Console.WriteLine(item.Name);
    foreach (var team in item.Teams)
    {
        Console.WriteLine(team.Name);
    }
}

}

async Task ListingTypes()
{
    // Listing types
    var coaches = await context.Coaches.ToListAsync();
    foreach (var coach in coaches)
    {
        Console.WriteLine(coach.Name);
    }

    var coach2Query = await context.Coaches.ToListAsync();
    foreach (var item in coach2Query)
    {
        Console.WriteLine(item.Name);
    }

    var key = Console.ReadKey();

    switch (key)
    {
        case { Key: ConsoleKey.A }:
            var coach = await context.Coaches.Include(c => c.Team).ElementAtAsync(1);
            Console.WriteLine($"coach Name {coach.Name} - Team Name {coach.Team.Name}");
            break;
        case { Key: ConsoleKey.B }:
            var coach2 = await context.Coaches.Include(c => c.Team).ElementAtAsync(2);
            Console.WriteLine($"coach Name {coach2.Name} - Team Name {coach2.Team.Name}");
            break;

        default:
            break;
    }
}

async Task InsertingData()
{
    /* var match = context.Matches.Add(new Match
    {
        HomeTeamId = 1,
        AwayTeamId = 2,
        HomeTeamScore = 2,
        AwayTeamScore = 1,
        CreatedDate = DateTime.Now,
        TicketPrice = 50,
        Date = DateTime.Now
    });*/

    var league = new league
    {
        Name = "Serie A",
        Teams = new List<Team>
    {
        new Team
        {
            Name="zamalek",

        },
        new Team
        {
            Name="somouha"
        }
    }
    };
    context.leagues.Add(league);
    context.SaveChanges();

    /*
    try
    {
        var coach = new Coach
        {
            Name = "Pep Guardiola",
            CreatedDate = DateTime.Now
        };

        await context.Coaches.AddAsync(coach);
        await context.SaveChangesAsync(); // CoachId is now available

        var team = new Team
        {
            Name = "Manchester City",
            CreatedDate = DateTime.Now,
            CoachId = coach.Id // Use the actual generated ID
        };

        await context.Teams.AddAsync(team);
        await context.SaveChangesAsync(); // Save the team
        Console.WriteLine("Coach and Team added successfully.");
    }
    catch (Exception ex)
    {
        Console.WriteLine("❌ Error occurred: " + ex.Message);
    }*/
}
async Task explicitLoading()
{

    var league = context.leagues.Include(q => q.Teams).ToList();

    foreach (var l in league)
    {
        Console.WriteLine(l.Name);
        foreach (var team in l.Teams)
        {
            Console.WriteLine(team.Name);
        }
    }
    var leagues = await context.leagues.FindAsync(4);
    await context.Entry(leagues)
        .Collection(q => q.Teams)
        .LoadAsync();

    foreach (var item in leagues.Teams)
    {
        Console.WriteLine(item.Name);
    }// eager loading
}
async Task sqlLinQ()
{
    var teamQuery = context.Teams.Skip(1)
                    .Take(1);
    var sqlQuery = teamQuery.ToQueryString();
    var teams = await teamQuery.ToListAsync();
    Console.WriteLine(sqlQuery); // this will print the sql query that will be executed
}

/*var allTeams = await context.Teams.ToListAsync();
foreach (var t in allTeams)

    Console.WriteLine($"{t.TeamId} - {t.Name}");*/
async Task listing()
{
    var teams = context.Teams.ToList();

    foreach (var item in teams)
    {
        Console.WriteLine(item.Name);
    }
    var firstTeam = await context.Teams.Where
                (team => team.Id > 1).ToListAsync();

    Console.WriteLine(firstTeam);
}
async Task aggregateFunctions()
{
    var numberTeams = await context.Teams.CountAsync(); // count
    var numberTeamsCondition = await context.Teams.CountAsync(team => team.Name.Contains("l"));  // count with condition
    var numberMax = await context.Teams.MaxAsync(team => team.Name.Contains("l")); // max number
    var numberMin = await context.Teams.MinAsync(team => team.CreatedDate); // min number for condition
    var numberAvg = await context.Teams.AverageAsync(team => team.Id); // avg
    var numberSum = await context.Teams.SumAsync(team => team.Id); // sum
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

    var orderedTeams = context.Teams.OrderByDescending(o => o.Id);
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
    var coach = listInsert.First();
    var str = $"{coach.Name} => {coach}";



   
    foreach (var item in listInsert)
    {
        await context.Coaches.AddAsync(item);
    }
    await context.SaveChangesAsync();

    //batch insert
    await context.Coaches.AddRangeAsync(listInsert);
    await context.SaveChangesAsync();

}

async Task updater()
{
    //updating
    var coach = await context.Coaches.FirstOrDefaultAsync(c => c.Name == "Zinedine Zidane");
    if (coach != null)
    {
        coach.Name = "Zinedine Zidane Updated";
        await context.SaveChangesAsync();
    }

    var updating = await context.Coaches.FindAsync(2); // 2 primary key value
    updating.Name = "Ahmed Hossam";// update name
    await context.SaveChangesAsync();

    // updating without tracking
    var updatingWithoutTracking = await context.Coaches.FirstOrDefaultAsync(context => context.TeamId == 2);
    updatingWithoutTracking.Name = "Mohamed Salah"; // update name
    context.Update(updatingWithoutTracking); // mark as modified
    await context.SaveChangesAsync();
}
async Task deleting()
{
    //deleting without tracking
    var coach = await context.Coaches.FirstOrDefaultAsync(c => c.Name == "Zinedine Zidane Updated");
    if (coach != null)
    {
        context.Coaches.Remove(coach);
        await context.SaveChangesAsync();
    }
    // deleting 

    var coach1 = await context.Coaches.FindAsync(1); // 1 primary key value
    context.Coaches.Remove(coach1);
    context.Entry(coach).State = EntityState.Deleted; // mark as deleted

    await context.SaveChangesAsync();
}

async Task ExecuteDeleteExecuteUpdate()
{
    // ExecuteDelete and ExecuteUpdate
    // ExecuteDelete is used to delete multiple records in one go
    // ExecuteUpdate is used to update multiple records in one go
    // update using execute update

    await context.Coaches.Where(c => c.Name == "Ahmed Hossam").ExecuteDeleteAsync(); // delete using execute delete
    await context.Coaches.Where(c => c.Name == "Mohamed Salah").ExecuteUpdateAsync(c => c.SetProperty(p => p.Name, "Mohamed Salah Updated")); // update using execute update
}


