using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Detector.Api.Users;

// This is our DetectorUser, we can modify this if we need
// to add custom properties to the user
public class DetectorUser : IdentityUser { }

