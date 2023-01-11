namespace AbyssiniaPocketLaw.API.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

[Table("laws")]
public partial class Law : BaseEntity
{
    public string Status { get; set; } = string.Empty;
    public string Entry { get; set; } = string.Empty;
    public string Jurisdiction { get; set; } = string.Empty;
}
