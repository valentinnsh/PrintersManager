using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Records;

public class PrinterRecord
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("id")]
    public int Id { get; set; } // TODO: Change to long
    [Column("name")]
    public string Name { get; set; }
    [Column("connection_type")]
    public ConnectionTypes ConnectionType { get; set; }
    [Column("mac")]
    public string? MAC { get; set; }
}