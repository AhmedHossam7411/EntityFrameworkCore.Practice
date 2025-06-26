namespace EFcore.domain;   // following with EF core -full tour udemy course

public class Match : BaseDomainModel
{
    public int HomeTeamId { get; set; }
    public int AwayTeamId { get; set; }
    public decimal TicketPrice { get; set; }
    public DateTime Date { get; set; }

    public Team HomeTeam { get; set; }
    public Team AwayTeam { get; set; }
    public int HomeTeamScore { get; set; }
    public int AwayTeamScore { get; set; }
}