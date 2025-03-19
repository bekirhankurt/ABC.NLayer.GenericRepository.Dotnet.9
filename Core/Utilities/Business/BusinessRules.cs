using System.Diagnostics;
using Core.Utilities.Results;

namespace Core.Utilities.Business;

public class BusinessRules
{
    public static IResult Run(params IResult[] logics)
    {
        return logics.FirstOrDefault(result => !result.Success);
    }
}