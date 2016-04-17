namespace OurMemory.Service.Model
{
    /// <summary>
    /// Search Model For Veterans
    /// </summary>
    public class SearchVeteranModel : SearchRequestModelBase
    {
        public int YearBirthStart { get; set; }
        public int YearBirthEnd { get; set; }
        public int YearDeathStart { get; set; }
        public int YearDeathEnd { get; set; }
        public int YearCallStart { get; set; }
        public int YearCallEnd { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string BirthPlace { get; set; }
    }
}