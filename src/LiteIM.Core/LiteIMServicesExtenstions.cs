using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

using System;
using System.Collections.Generic;
using System.Text;

namespace LiteIM
{
    public static class LiteIMServicesExtenstions
    {
        public static IServiceCollection AddLiteIMCore<TImClient, TImClientOptions>(this IServiceCollection services, TImClientOptions options)
            where TImClient : class, IImClient, new()
            where TImClientOptions : class, IImClientOptions, new()
        {
            services.TryAddSingleton<IImClientOptions>(options);
            services.TryAddSingleton<IImClient, TImClient>();

            return services;
        }
    }
}
