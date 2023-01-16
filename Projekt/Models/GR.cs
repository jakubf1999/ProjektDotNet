using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Projekt.Models;
public class GR
{
    public int GameId { get; set; }
    public string GameName { get; set; }
    [Range(0, 10.5)]
    [RegularExpression("[0-9]|10")]
    public decimal Score { get; set; }
}