//
using System.ComponentModel.DataAnnotations;

namespace Detector.Contract.DTO;

public class UserItem
{
    [Required]
    public string Username { get; set; } = default!;

    [Required]
    public string Password { get; set; } = default!;
}

