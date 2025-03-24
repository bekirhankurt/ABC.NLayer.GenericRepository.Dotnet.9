using Core.Entities.Concrete;
using Core.Repository.EntityFrameworkCore;
using Repository.Abstract;
using Repository.Concrete.Contexts;

namespace Repository.Concrete;

public class UserRepository : EntityFrameworkRepositoryBase<User, EntityFrameworkContext>, IUserRepository
{
    public List<OperationClaim> GetClaims(User user)
    {
        using var context = new EntityFrameworkContext();
        var result = from operationClaim in context.OperationClaims
                     join userOperationClaim in context.UserOperationClaims
                         on operationClaim.Id equals userOperationClaim.OperationClaimId
                     where userOperationClaim.UserId == user.Id
                     select new OperationClaim
                     {
                         Id = operationClaim.Id,
                         Name = operationClaim.Name
                     };
        return result.ToList();
    }
}