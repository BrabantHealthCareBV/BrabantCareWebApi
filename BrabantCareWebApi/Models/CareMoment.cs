namespace BrabantCareWebApi.Models
{
    public class CareMoment
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public byte[]? Image { get; set; }
        public int? DurationInMinutes { get; set; }
    }
}