

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Detector.Contract.Models;

public class DetectorData
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public string Name { get; set; } = null;

    public int Version { get; set; }

    [Required]
    public DetectorDetails DetectorDetails { get; set; }

}
