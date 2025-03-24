using Core.Entities.Concrete;

namespace Service.Abstract;

public interface IUserService
{
    List<OperationClaim> GetClaims(User user);
    void Add(User user);
    Task<User> GetByMail(string email);
}