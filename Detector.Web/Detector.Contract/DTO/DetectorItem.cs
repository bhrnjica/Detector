//
using Detector.Contract.Models;
using System.ComponentModel.DataAnnotations;

namespace Detector.Contract.DTO;

public class DetectorItem
{
    public int Id { get; set; }

    public string Name { get; set; }= null;

    public int Version { get; set; } = 0;

    public string Notes { get; set; } = null;
}

