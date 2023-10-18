using System.ComponentModel.DataAnnotations;

namespace API.Dtos;

public class UserDto
{
    [Required]
    public string Email { get; set; }
    [Required]
    public string DisplayName { get; set; }
    [Required]
    public string Token { get; set; }
}
