namespace AbyssiniaPocketLaw.API.Entities;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


[Table("users")]
public partial class User
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("id")]
    public int Id { get; set; }
    [Column("user_name")]
    public string? UserName { get; set; }
    [Column("pass_word")]
    public string? PassWord { get; set; }
    [Column("Full_Name")]
    public string? FullName { get; set; }
    public string? Role { get; set; }
}
