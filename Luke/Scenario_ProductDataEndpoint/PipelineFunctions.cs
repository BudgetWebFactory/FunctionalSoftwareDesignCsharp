using System;
using System.Collections.Generic;
using System.Linq;

namespace LukeCsharpFPScenarios.Scenario_ProductDataEndpoint
{
    public static class PipelineFunctions<TContract>
    {
        public static TContract Execute(TContract source, List<Func<TContract, TContract>> steps)
        {
            return steps.Aggregate(source, (temp, next) => next(temp));
        }

        public static TContract ExecuteSafely(
            TContract source,
            List<(Func<TContract, TContract> fn, ErrorFunctions.ErrorFunction errorFn)> steps)
        {
            return steps.Aggregate(
                source,
                (temp, next) =>
                {
                    try
                    {
                        return next.fn(temp);
                    }
                    catch (Exception e)
                    {
                        next.errorFn(e);

                        return temp;
                    }
                });
        }
    }
}