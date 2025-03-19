using Core.Repository.EntityFrameworkCore;
using Entity.Concrete;
using Repository.Abstract;
using Repository.Concrete.Contexts;

namespace Repository.Concrete;

public class ProductRepository: EntityFrameworkRepositoryBase<Product, EntityFrameworkContext>, IProductRepository
{
    
}