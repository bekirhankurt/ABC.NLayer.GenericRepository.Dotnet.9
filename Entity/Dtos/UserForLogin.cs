using Core.Entities;

namespace Entity.Dtos;

public class UserForLogin : IDto
{
    public string Email { get; set; }
    public string Password { get; set; }

}