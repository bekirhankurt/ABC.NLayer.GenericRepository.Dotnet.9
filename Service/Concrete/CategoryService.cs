using Core.Utilities.Results;
using Entity.Concrete;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Repository.Abstract;
using Service.Abstract;

namespace Service.Concrete;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;

    public CategoryService(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<IDataResult<IEnumerable<Category>>> GetAll()
    {
        var categories = await _categoryRepository.GetAllAsync();
        return new SuccessDataResult<IEnumerable<Category>>(categories.ToList());
    }
}