namespace EFcore.domain   // following with EF core -full tour udemy course
{
    public abstract class BaseDomainModel //provides base props to data models
    {
        public DateTime CreatedDate { get; set; }
    }
}
