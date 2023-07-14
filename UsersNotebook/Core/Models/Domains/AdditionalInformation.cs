namespace UsersNotebook.Core.Models.Domains
{
    public class AdditionalInformation
    {
        public int AdditionalInformationId { get; set; }
        public string InformationType { get; set; }
        public string InformationValue { get; set; }
        public int? UserId { get; set; }
        public User? User { get; set; }
    }
}
