using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

using System;
using System.Collections.Generic;
using System.Text;

namespace LiteIM
{
    public static class LiteIMCsRedisCoreServicesExtenstions
    {
        public static IServiceCollection AddLiteIMCsRedis<TImClient, TImClientOptions>(this IServiceCollection services, TImClientOptions options)
            where TImClient : class, IImClient, new()
            where TImClientOptions : class, ICsRedisCoreImClientOptions, new()
        {
            services.TryAddSingleton<ICsRedisCoreImClientOptions>(options);

            services.AddLiteIMCore<TImClient, TImClientOptions>(options);
            
            return services;
        }
    }
}
