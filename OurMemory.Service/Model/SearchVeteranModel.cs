namespace OurMemory.Service.Model
{
    /// <summary>
    /// Search Model For Veterans
    /// </summary>
    public class SearchVeteranModel : SearchRequestModelBase
    {
        public int DateBirthStart { get; set; }
        public int DateBirthEnd { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
    }
}