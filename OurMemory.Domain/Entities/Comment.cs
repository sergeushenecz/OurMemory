namespace OurMemory.Domain.Entities
{
    public class Comment : DomainObject
    {
        public string Message { get; set; }
        public Article Article { get; set; }
        public User User { get; set; }
    }
}