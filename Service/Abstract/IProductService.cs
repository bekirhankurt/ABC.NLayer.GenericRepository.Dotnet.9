using Core.Utilities.Results;
using Entity.Concrete;

namespace Service.Abstract;

public interface IProductService
{
    Task<IDataResult<Product>> GetById(int id);
    Task<IDataResult<IEnumerable<Product>>> GetAll();
    Task<IDataResult<IEnumerable<Product>>> GetAllByCategory(int categoryId);
    IResult Add(Product product);
    IResult Update(Product product);
    IResult Delete(Product product);

    IResult TransactionalOperation(Product product);
}