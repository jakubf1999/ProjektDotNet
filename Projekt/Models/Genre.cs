using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Projekt.Models
{
    [Table("Genres")]
    public class Genre
    {

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(25)]
        [Display(Name = "Nazwa")]
        public string Name { get; set; }

        [Display(Name = "Gry")]
        public ICollection<Game>? Games { get; set; }
    }
}
