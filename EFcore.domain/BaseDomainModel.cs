namespace EFcore.domain   // following with EF core -full tour udemy course
{
    public abstract class BaseDomainModel //provides base props to data models
    {
        public int Id { get; set; }  // Primary key for all tables
        public DateTime ModifiedDate { get; set; }  
        public DateTime CreatedDate { get; set; }
        public string? CreatedBy { get; set; } // who created the record
        public string? ModifiedBy { get; set; } // who modified the record last time
    }
}
