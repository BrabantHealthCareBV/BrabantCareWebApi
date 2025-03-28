namespace BrabantCareWebApi.Models
{
    public class Guardian
    {
        public Guid ID;
        public string FirstName;
        public string LastName;
        public List<Guid> PatientIDs;
    }
}