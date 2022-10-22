namespace AbyssiniaPocketLaw.API.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


[Table("cassation")]
public partial class Cassation
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("id")]
    public int Id { get; set; } 
    public string Title { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public string Volume { get; set; } = string.Empty;
    public string Decision { get; set; } = string.Empty;
    public string Given { get; set; } = string.Empty;
    public string Download { get; set; } = string.Empty;
    public string Keywords { get; set; } = string.Empty;
}
