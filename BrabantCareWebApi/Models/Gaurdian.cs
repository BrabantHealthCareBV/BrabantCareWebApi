namespace BrabantCareWebApi.Models
{
    public class Guardian
    {
        public Guid ID { get; set; }
        public string? UserID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}