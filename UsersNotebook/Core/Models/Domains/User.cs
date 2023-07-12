using System.ComponentModel.DataAnnotations;
using UsersNotebook.Attributes;

namespace UsersNotebook.Core.Models.Domains
{
    public class User
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Pole Imię jest wymagane.")]
        [MaxLength(50)]
        [RegularExpression(@"^[A-Za-zęóąśłżźćńĘÓĄŚŁŻŹĆŃ]+$", ErrorMessage = "Imię może zawierać tylko litery.")]
        [FirstLetterCapital(ErrorMessage = "Pierwsza litera powinna być napisana z dużej litery.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Pole Nazwisko jest wymagane.")]
        [MaxLength(150)]
        [RegularExpression(@"^[A-Za-zęóąśłżźćńĘÓĄŚŁŻŹĆŃ]+$", ErrorMessage = "Imię może zawierać tylko litery.")]
        [FirstLetterCapital(ErrorMessage = "Pierwsza litera powinna być napisana z dużej litery.")]
        public string Surname { get; set; }
        [Required(ErrorMessage = "Pole Data Urodzenia jest wymagane.")]
        public DateTime DateOfBirth { get; set; }
        [Required(ErrorMessage = "Pole Płeć jest wymagane.")]
        public string Gender { get; set; }
        public ICollection<AdditionalInformation>? AdditionalInformations { get; set; }
    }
}
