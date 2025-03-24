using Core.Entities.Concrete;
using Core.Utilities.Results;
using Core.Utilities.Security.Hashing;
using Core.Utilities.Security.Jwt;
using Entity.Dtos;
using Service.Abstract;
using Service.Constants;

namespace Service.Concrete;

public class AuthService(IUserService userService, ITokenHelper tokenHelper) : IAuthService
{
  

    public IDataResult<User> Register(UserForRegister userForRegister, string password)
    {
        byte[] passwordSalt, passwordHash;
        HashingHelper.CreatePasswordHash(password, out passwordHash, out passwordSalt);
        var user = new User
        {
            Email = userForRegister.Email,
            FirstName = userForRegister.FirstName,
            LastName = userForRegister.LastName,
            PasswordHash = passwordHash,
            PasswordSalt = passwordSalt,
            Status = true,
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now
        };

        userService.Add(user);
        return new SuccessDataResult<User>(user, message: Messages.UserRegistered);
    }

    public IDataResult<User> Login(UserForLogin userForLogin)
    {
        var userCheck = userService.GetByMail(userForLogin.Email);
        if (userCheck == null)
        {
            return new ErrorDataResult<User>(Messages.UserNotFound);
        }

        if (!HashingHelper.VerifyPasswordHash(userForLogin.Password, userCheck.PasswordHash, userCheck.PasswordSalt))
        {
            return new ErrorDataResult<User>(Messages.PasswordError);
        }

        return new SuccessDataResult<User>(Messages.SuccessfulLogin);
    }

    public IResult UserExists(string email)
    {

        if (userService.GetByMail(email) != null)
        {
            return new ErrorResult(Messages.UserAlreadyExists);
        }
        return new SuccessResult();
    }

    public IDataResult<AccessToken> CreateAccessToken(User user)
    {
        var claims = userService.GetClaims(user);
        var accessToken = tokenHelper.CreateToken(user, claims);
        return new SuccessDataResult<AccessToken>(accessToken, Messages.AccessTokenCreated);
    }
}