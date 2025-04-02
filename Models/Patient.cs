namespace BrabantCareWebApi.Models
{
    public class Patient
    {
        public Guid ID { get; set; }
        public string? UserID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? Birthdate { get; set; } 
        public DateTime? NextAppointmentDate { get; set; }
        public Guid GuardianID { get; set; }
        public Guid TreatmentPlanID { get; set; }
        public Guid? DoctorID { get; set; }
    }
}
