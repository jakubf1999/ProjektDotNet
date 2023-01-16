using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;

namespace Projekt.Models
{
    [Table("Games")]
    public class Game
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(25)]
        [Display(Name = "Nazwa")]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Data premiery")]
        public DateTime Date { get; set; }

        public int? PublisherId { get; set; }
        [Display(Name = "Wydawca")]
        [ForeignKey("PublisherId")]
        public Publisher? Publisher { get; set; }

        [NotMapped]
        public List<int>? GenreIds { get; set; }

        [Display(Name = "Gatunki")]
        public ICollection<Genre>? Genres { get; set; }

        [Display(Name = "Konta")]
        public ICollection<Account>? Accounts { get; set; }

        public ICollection<Review>? Reviews { get; set; }




    }
}
