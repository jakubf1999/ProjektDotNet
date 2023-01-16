using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Projekt.Models;
[Table("Reviews")]
[PrimaryKey(nameof(AccountId), nameof(GameId))]
public class Review
{
    public int AccountId { get; set; }
    public int GameId { get; set; }
    [Range(0, 10.5)]
    [RegularExpression("[0-9]|10")]
    public decimal Score { get; set; }
    [ForeignKey("AccountId")]
    public Account? Account { get; set; }
    [ForeignKey("GameId")]
    public Game? Game { get; set; }
}