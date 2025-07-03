using EFcore.domain;

namespace EFcore.domain;

public class Team : BaseDomainModel
{
    public string Name { get; set; }

    public league league { get; set; } // for one to many relationship , nav property to League
    public int? LeagueId { get; set; } // foreign key to League

    public int? CoachId { get; set; } //team needs a coach 
    public Coach Coach { get; set; } // navigation property to Coach .
         // For SQL Server Only
         //[Timestamp]
         //public byte[] Version { get; set; }

    public List<Match> HomeMatches { get; set; } = new List<Match>() { };
    public List<Match> AwayMatches { get; set; } = new List<Match>() { };
}