
namespace AbyssiniaPocketLaw.API.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


[Table("laws")]
public partial class Law
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("id")]
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public string Entry { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public string Jurisdiction { get; set; } = string.Empty;
    public string Download { get; set; } = string.Empty;
    public string Keywords { get; set; } = string.Empty;
}
