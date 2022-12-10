using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
namespace Detector.Shared.Entities;

// This is the DTO used to exchange username and password details to 
// the create user and token endpoints
public class UserInfo
{
    [Required]
    public string Username { get; set; } = default!;

    [Required]
    public string Password { get; set; } = default!;
}
