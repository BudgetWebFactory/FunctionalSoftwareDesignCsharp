using System;
using System.Linq;

namespace LukeCsharpFPScenarios.Scenario_ProductDataEndpoint
{
    public static class CompositionFunctions
    {
        public static Func<TBind, TBind>
            GetComposedFunc<TBind>(params (Func<TBind, TBind>, Func<TBind, Exception, TBind>)[] funcs)
        {
            return x =>
            {
                return funcs.Aggregate(x, (state, step) =>
                {
                    try
                    {
                        return step.Item1(state);
                    }
                    catch (Exception e)
                    {
                        return step.Item2(state, e);
                    }
                });
            };
        }
    }
}