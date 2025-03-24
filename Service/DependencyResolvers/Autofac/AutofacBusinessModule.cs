using Autofac;
using Autofac.Extras.DynamicProxy;
using Castle.DynamicProxy;
using Core.Utilities.Interceptors;
using Core.Utilities.Security.Jwt;
using Repository.Abstract;
using Repository.Concrete;
using Service.Abstract;
using Service.Concrete;

namespace Service.DependencyResolvers.Autofac;

public class AutofacBusinessModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<ProductService>().As<IProductService>();

        builder.RegisterType<ProductRepository>().As<IProductRepository>();

        builder.RegisterType<CategoryService>().As<ICategoryService>();
        builder.RegisterType<CategoryRepository>().As<ICategoryRepository>();

        builder.RegisterType<UserService>().As<IUserService>();
        builder.RegisterType<UserRepository>().As<IUserRepository>();

        builder.RegisterType<AuthService>().As<IAuthService>();
        builder.RegisterType<JwtHelper>().As<ITokenHelper>();

        var assembly = System.Reflection.Assembly.GetExecutingAssembly();

        builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces()
            .EnableInterfaceInterceptors(new ProxyGenerationOptions()
            {
                Selector = new AspectInterceptorSelector()
            }).SingleInstance();

    }
}