using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;

namespace Projekt.Models
{
    [Table("Accounts")]
    public class Account
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(25)]
        [Display(Name = "Login")]
        public string Login { get; set; }

        [Required]
        [MaxLength(25)]
        [DataType(DataType.Password)]
        [Display(Name = "Hasło")]
        public string Password { get; set; }

        [Required(AllowEmptyStrings = true)]
        [MaxLength(50)]
        [RegularExpression(@"^[\w-_]+(\.[\w!#$%'*+\/=?\^`{|}]+)*@((([\-\w]+\.)+[a-zA-Z]{2,20})|(([0-9]{1,3}\.){3}[0-9]{1,3}))$", ErrorMessage = "Niepoprawny adres e-mail!")]
        [Display(Name = "E-mail")]
        public string Email { get; set; }

        public int? UserId { get; set; }
        [ForeignKey("UserId")]
        public User? User { get; set; }

        [Display(Name = "Gry")]
        public ICollection<Game>? Games { get; set; }
        public ICollection<Review>? Reviews { get; set; }

    }
}
