using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Logging;
using Core.CrossCuttingConcerns.Logging.Log4Net;
using Core.Utilities.Interceptors;
using Core.Utilities.Messages;
using log4net.Repository.Hierarchy;
using Microsoft.AspNetCore.SignalR.Protocol;

namespace Core.Aspects.Autofac.Logging;

public class LogAspect : MethodInterception
{
    private readonly LoggerServiceBase _loggerServiceBase;

    public LogAspect(Type loggerService)
    {
        if (loggerService.BaseType != typeof(LoggerServiceBase))
        {
            throw new System.Exception(AspectMessages.WrongLoggerType);
        }

        _loggerServiceBase = (LoggerServiceBase) Activator.CreateInstance(loggerService;
    }

    protected override void OnBefore(IInvocation invocation)
    {
        _loggerServiceBase.Info(GetLogDetail(invocation));
    }

    private LogDetail GetLogDetail(IInvocation invocation)
    {
        var logParameters = invocation.Arguments.Select((t, i) 
            => new LogParameter { Name = invocation.GetConcreteMethod().GetParameters()[i].Name, Type = t.GetType().Name, Value = t }).ToList();

        return new LogDetail
        {
            MethodName = invocation.Method.Name,
            LogParameters = logParameters
        };
    }
}