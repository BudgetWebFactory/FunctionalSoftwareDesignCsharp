using System;
using System.Linq;

namespace LukeCsharpFPScenarios.Scenario_ProductDataEndpoint
{
    public static class CompositionFunctions
    {
        public static Func<TBind, TBind> GetComposedFunc<TBind>(params Func<TBind, TBind>[] funcs)
        {
            return x =>
            {
                return funcs.Aggregate(x, (context, fn) => fn(context));
            };
        }
    }
}