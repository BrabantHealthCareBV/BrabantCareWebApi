
namespace BrabantCareWebApi.Models
{
    public class Doctor
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public string Specialization { get; set; }
        public List<Guid> PatientIDs { get; set; } // optional, ensure this is handled if necessary
    }

}