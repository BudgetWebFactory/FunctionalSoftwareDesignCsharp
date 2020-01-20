using System;
using System.Linq;

namespace LukeCsharpFPScenarios.Scenario_ProductDataEndpoint
{
    public static class CompositionFunctions
    {
        public static (Func<TBind, TBind>, Func<TBind, Exception, TBind>)
            GetComposedFunc<TBind>(Func<TBind, Exception, TBind> errorFunc, params (Func<TBind, TBind>, Func<TBind, Exception, TBind>)[] funcs)
        {
            return (x =>
            {
                TBind state = x;

                foreach (var func in funcs)
                {
                    try
                    {
                        state = func.Item1(state);
                    }
                    catch (Exception e)
                    {
                        state = func.Item2(state, e);
                        break; // optionally we could change this behavior (ex. moving along to the next func in case of an error)
                    }
                }

                return state;
            }, (x, exception) => errorFunc(x, exception));
        }
    }
}