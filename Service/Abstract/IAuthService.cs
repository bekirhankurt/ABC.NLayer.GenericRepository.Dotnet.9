using Core.Entities.Concrete;
using Core.Utilities.Results;
using Core.Utilities.Security.Jwt;
using Entity.Dtos;

namespace Service.Abstract;

public interface IAuthService
{
    IDataResult<User> Register(UserForRegister userForRegister, string password);
    IDataResult<User> Login(UserForLogin userForLogin);
    IResult UserExists(string email);
    IDataResult<AccessToken> CreateAccessToken(User user);
}