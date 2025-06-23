namespace EFcore.domain;   // following with EF core -full tour udemy course

public class Team : BaseDomainModel
{
    public int TeamId { get; set; }
    public string? Name { get; set; }  // ? , can be null
    

}
