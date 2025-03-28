namespace BrabantCareWebApi.Models
{
    public class TreatmentPlan
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public List<Guid>? PatientIDs { get; set; }
        public List<Guid>? CareMomentIDs { get; set; }

    }
}