using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Records;

public class InstallationRecord
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("id")]
    public int Id { get; set; }
    [Column("external_id")]
    public Guid ExternalId { get; set; }
    [Column("local_name")]
    public string Name { get; set; }
    [Column("local_number")]
    public byte LocalNumber { get; set; }
    [Column("is_default")]
    public bool IsDefault { get; set; }
    [Column("printer_id")]
    public int PrinterId { get; set; }
    [Column("branch_id")]
    public int BranchId { get; set; }
}