using System;
using System.Collections.Generic;
using System.Linq;

namespace LukeCsharpFPScenarios.Scenario_ProductDataEndpoint
{
    /// <summary>
    /// This class functions acts as a convenience way to enable pipeline processing
    /// </summary>
    /// <typeparam name="TContract"></typeparam>
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
            // this "safe" implementation demands error fns which will be processed in case of an error and
            // provided by the consumer of the pipeline to preserve control over it by the caller

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