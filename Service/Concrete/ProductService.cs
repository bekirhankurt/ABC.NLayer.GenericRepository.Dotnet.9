using Core.Utilities.Business;
using Core.Utilities.Results;
using Entity.Concrete;
using Repository.Abstract;
using Service.Abstract;
using Service.Constants;

namespace Service.Concrete;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;
    private ICategoryService _categoryService;

    public ProductService(IProductRepository productRepository, ICategoryService categoryService)
    {
        _productRepository = productRepository;
        _categoryService = categoryService;
    }

    public async Task<IDataResult<Product>> GetById(int id)
    {
        var product = _productRepository.GetAsync(p => p.Id == id).Result;
        return new SuccessDataResult<Product>(await _productRepository.GetAsync(p => p.Id == id));
    }

    public async Task<IDataResult<IEnumerable<Product>>> GetAll()
    {
        var products = await _productRepository.GetAllAsync();
        return new SuccessDataResult<IEnumerable<Product>>(products.ToList());
    }

    public async Task<IDataResult<IEnumerable<Product>>> GetAllByCategory(int categoryId)
    {
        var filteredProducts = await _productRepository.GetAllAsync(p => p.CategoryId == categoryId);
        return new SuccessDataResult<IEnumerable<Product>>(filteredProducts.ToList());
    }

    public IResult Add(Product product)
    {
        var result = BusinessRules.Run( CheckIfProductNameExists(product.Name), CheckIfCategoryIsEnabled());

        if (result != null)
        {
            return result;
        }
        _productRepository.Add(product);
        return new SuccessResult(Messages.ProductAdded);
    }

    private IResult CheckIfCategoryIsEnabled()
    {
        var result = _categoryService.GetAll().Result;
        if (result.Data.Count() <10)
        {
            return new ErrorResult(Messages.ProductNameAlreadyExists);
        }

        return new SuccessResult();
    }

    private IResult CheckIfProductNameExists(string productName)
    {
        var result =  _productRepository.GetAllAsync(p => p.Name == productName).Result;
        if (result.Any())
        {
            return new ErrorResult(Messages.ProductNameAlreadyExists);
        }

        return new SuccessResult();
    }

    public IResult Update(Product product)
    {
        _productRepository.Update(product);
        return new SuccessResult(Messages.ProductUpdated);
    }

    public IResult Delete(Product product)
    {
        _productRepository.Delete(product);
        return new SuccessResult(Messages.ProductDeleted);
    }

    public IResult TransactionalOperation(Product product)
    {
        _productRepository.Update(product);
        _productRepository.Add(product);
        return new SuccessResult(Messages.ProductUpdated);
    }
}