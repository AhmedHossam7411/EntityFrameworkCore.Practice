namespace EFcore.domain;   // following with EF core -full tour udemy course

public class Team : BaseDomainModel
{
    public int Id { get; set; }  // Primary key
    public string? Name { get; set; }
    public int LeagueId { get; set; }
    public int CoachId { get; set; }
}
