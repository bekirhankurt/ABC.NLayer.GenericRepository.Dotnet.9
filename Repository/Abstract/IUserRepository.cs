using Core.Entities.Concrete;
using Core.Repository;

namespace Repository.Abstract;

public interface IUserRepository : IEntityRepository<User>
{
    List<OperationClaim> GetClaims(User user);
}