using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;



namespace Projekt.Models
{
    [Table("Users")]
    public class User
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(25)]
        [Display(Name = "Imię")]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(25)]
        [Display(Name = "Nazwisko")]
        public string LastName { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Data urodzenia")]
        public DateTime BirthDate { get; set; }
        
        [Required(AllowEmptyStrings = true)]
        [RegularExpression(@"^\d{9}$", ErrorMessage = "Numer telefonu musi zawierać 9 cyfr!")]
        [Display(Name = "Numer telefonu")]
        public string Number { get; set; }


        [Required(AllowEmptyStrings = true)]
        [RegularExpression(@"^\d{11}$", ErrorMessage = "Numer pesel musi zawierać 1 cyfr!")]
        [Display(Name = "Numer pesel")]
        public string Pesel { get; set; }

        [Required]
        [MaxLength(100)]
        [Display(Name = "Adres")]
        public string Address { get; set; }

        public Account? Account { get; set; }

        [NotMapped]
        public string FL
        {
            get
            {
                return FirstName + " " + LastName + " (" + Id + ")";
            }
        }

    }
}
