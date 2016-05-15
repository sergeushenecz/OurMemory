namespace OurMemory.Domain.Entities
{
    public class Comment : DomainObject
    {
        public string Message { get; set; }
        public virtual Article Article { get; set; }
        public virtual User User { get; set; }
    }
}