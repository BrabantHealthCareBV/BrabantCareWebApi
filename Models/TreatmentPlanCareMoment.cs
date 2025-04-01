namespace BrabantCareWebApi.Models
{
    public class TreatmentPlanCareMoment
    {
        public Guid TreatmentPlanID { get; set; }
        public Guid CareMomentID { get; set; }
        public int Order { get; set; }

    }
}