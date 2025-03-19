using Core.Utilities.Results;
using Entity.Concrete;

namespace Service.Abstract;

public interface ICategoryService
{
    Task<IDataResult<IEnumerable<Category>>> GetAll();
}