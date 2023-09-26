using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace webapi.Database.Model;

public class ShortUrlModel
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }
    public required string ShortUrl { get; set; }
    public required string Url { get; set; }
    public required DateTime CreatedAt { get; set; }
    public required long CreatedBy { get; set; }
}