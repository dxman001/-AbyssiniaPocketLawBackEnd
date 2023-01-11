namespace AbyssiniaPocketLaw.API.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("cassation")]
public partial class Cassation: BaseEntity
{
    public string Volume { get; set; } = string.Empty;
    public string Decision { get; set; } = string.Empty;
    public string Given { get; set; } = string.Empty;
}
