using Microsoft.AspNetCore.Mvc.Filters;
using siberianoilrush_server.Exceptions;
using siberianoilrush_server.Extension;
using System;

namespace siberianoilrush_server.Filters
{
    public class GameLogicFilterAttribute : Attribute, IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if(context.Exception is NoMoneyException)
                context.Result = Result.Error("filter treatment", SorResources.Models.ResponseCode.NoMoney);
            else if(context.Exception is GameException)
                context.Result = Result.Error($"filter treatment {context.Exception.Message}", SorResources.Models.ResponseCode.Failed);
        }
    }
}
