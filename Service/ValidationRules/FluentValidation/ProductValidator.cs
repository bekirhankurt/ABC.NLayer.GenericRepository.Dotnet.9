using Entity.Concrete;
using FluentValidation;

namespace Service.ValidationRules.FluentValidation;

public class ProductValidator: AbstractValidator<Product>
{
    public ProductValidator()
    {
        RuleFor(p => p.Name).NotEmpty();
        RuleFor(p => p.Name).Length(2, 30);
        RuleFor(p => p.UnitPrice).NotEmpty();
        RuleFor(p => p.UnitPrice).GreaterThanOrEqualTo(1);
        RuleFor(p => p.UnitPrice).GreaterThanOrEqualTo(10).When(p => p.CategoryId == 1);
        RuleFor(p => p.Name).Must(RuleForStartName);

    }

    private static bool RuleForStartName(string arg)
    {
        return arg.StartsWith($"C");
    }
}