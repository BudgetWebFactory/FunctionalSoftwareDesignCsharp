using System;
using Microsoft.Extensions.DependencyInjection;
using static Dg.Framework.FeatureToggles.Api.FeatureToggleApiController;

namespace Dg.Framework.FeatureToggles.IoC
{
    public static class Config
    {
        public static void Configure(IServiceCollection services)
        {
            services.AddTransient<LoadFeatureToggles>((IServiceProvider sp) => Persistence.LoadAll);
        }
    }
}