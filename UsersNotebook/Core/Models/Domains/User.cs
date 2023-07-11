using System.ComponentModel.DataAnnotations;

namespace UsersNotebook.Core.Models.Domains
{
    public class User
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Pole Imię jest wymagane.")]
        [MaxLength(50)]
        [RegularExpression(@"^[A-Za-z]+$", ErrorMessage = "Imię może zawierać tylko litery.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Pole Nazwisko jest wymagane.")]
        [MaxLength(150)]
        [RegularExpression(@"^[A-Za-z]+$", ErrorMessage = "Imię może zawierać tylko litery.")]
        public string Surname { get; set; }
        [Required(ErrorMessage = "Pole Data Urodzenia jest wymagane.")]
        public DateTime DateOfBirth { get; set; }
        [Required(ErrorMessage = "Pole Płeć Urodzenia jest wymagane.")]
        public string Gender { get; set; }
        public ICollection<AdditionalInformation>? AdditionalInformations { get; set; }
    }
}
