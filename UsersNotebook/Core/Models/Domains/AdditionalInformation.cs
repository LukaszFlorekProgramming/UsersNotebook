using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace UsersNotebook.Core.Models.Domains
{
    public class AdditionalInformation
    {
        public int AdditionalInformationId { get; set; }
        [DisplayName("Typ informacji")]
        [RegularExpression(@"^[A-Za-zęóąśłżźćńĘÓĄŚŁŻŹĆŃ ]*$", ErrorMessage = "Typ informacji może zawierać tylko litery.")]
        public string InformationType { get; set; }
        [DisplayName("Wartość informacji")]
        [RegularExpression("^[a-zA-Z0-9ąćęłńóśźżĄĆĘŁŃÓŚŹŻ ]*$", ErrorMessage = "Wartość informacji może zawierać tylko litery i cyfry.")]
        public string InformationValue { get; set; }
        public int UserId { get; set; }
        public User? User { get; set; }
    }
}
