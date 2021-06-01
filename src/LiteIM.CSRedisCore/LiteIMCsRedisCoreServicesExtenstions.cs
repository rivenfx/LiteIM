using LiteIM;
using Microsoft.Extensions.DependencyInjection.Extensions;

using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class LiteIMCsRedisCoreServicesExtenstions
    {
        /// <summary>
        /// 添加 LiteIM CsRedis 到依赖注入容器
        /// </summary>
        /// <typeparam name="TImClientOptions">Client配置类型</typeparam>
        /// <param name="services"></param>
        /// <param name="options">client 配置</param>
        /// <returns></returns>
        public static IServiceCollection AddLiteIMCsRedisCore<TImClientOptions>(this IServiceCollection services, TImClientOptions options)
            where TImClientOptions : class, ICsRedisCoreImClientOptions
        {
            services.AddLiteIMCsRedisCore<TImClientOptions>((s) =>
            {
                return options;
            });

            return services;
        }

        /// <summary>
        /// 添加 LiteIM CsRedis 到依赖注入容器
        /// </summary>
        /// <typeparam name="TImClientOptions">Client配置类型</typeparam>
        /// <param name="services"></param>
        /// <param name="optionConfig">client 配置获取函数</param>
        /// <returns></returns>
        public static IServiceCollection AddLiteIMCsRedisCore<TImClientOptions>(this IServiceCollection services, Func<IServiceProvider, TImClientOptions> optionConfig)
            where TImClientOptions : class, ICsRedisCoreImClientOptions
        {
            services.TryAddSingleton<ICsRedisCoreImClientOptions>(optionConfig);

            services.AddLiteIMCore<CsRedisCoreImClient, TImClientOptions>(optionConfig);

            return services;
        }
    }
}
