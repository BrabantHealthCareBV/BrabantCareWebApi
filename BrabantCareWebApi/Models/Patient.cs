namespace BrabantCareWebApi.Models
{
    public class Patient
    {
        public Guid ID { get; set; }
        public string? UserID { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public DateTime? birthdate { get; set; } 
        public DateTime? nextAppointmentDate { get; set; }
        public Guid guardianID { get; set; }
        public Guid treatmentPlanID { get; set; }
        public Guid? doctorID { get; set; }
        public int gameState { get; set; }
        public int score { get; set; }
    }
}
