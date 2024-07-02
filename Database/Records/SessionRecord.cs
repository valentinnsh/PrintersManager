using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Records;

public class SessionRecord
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("id")]
    public int Id { get; set; }
    [Column("name")]
    public string Name { get; set; }
    [Column("pages")]
    public short Pages { get; set; }
    [Column("status")]
    public SessionStatuses Status { get; set; }
    [Column("installation_id")]
    public int InstallationId { get; set; }
    [Column("employee_id")]
    public int EmployeeId { get; set; }
}