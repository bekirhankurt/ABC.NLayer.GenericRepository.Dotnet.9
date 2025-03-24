using Core.Entities.Concrete;
using Repository.Abstract;
using Service.Abstract;

namespace Service.Concrete;

public class UserService(IUserRepository userRepository) : IUserService
{
   

    public List<OperationClaim> GetClaims(User user)
    {
        return userRepository.GetClaims(user);
    }

    public void Add(User user)
    {
        userRepository.Add(user);
    }

    public async Task<User> GetByMail(string email)
    {
        return await userRepository.GetAsync(m => m.Email == email);
    }
}