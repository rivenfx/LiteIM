using LiteIM;
using Microsoft.Extensions.DependencyInjection.Extensions;

using System;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class LiteIMStackExchangeRedisServicesExtenstions
    {
        /// <summary>
        /// 添加 LiteIM StackExchange.Redis 到依赖注入容器
        /// </summary>
        /// <typeparam name="TImClientOptions">Client 配置类型</typeparam>
        /// <param name="services"></param>
        /// <param name="options">client 配置</param>
        /// <returns></returns>
        public static IServiceCollection AddLiteIMStackExchangeRedis<TImClientOptions>(this IServiceCollection services, TImClientOptions options)
            where TImClientOptions : class, IStackExchangeRedisImClientOptions
        {
            services.AddLiteIMStackExchangeRedis<TImClientOptions>((s) =>
            {
                return options;
            });

            return services;
        }

        /// <summary>
        /// 添加 LiteIM StackExchange.Redis 到依赖注入容器
        /// </summary>
        /// <typeparam name="TImClientOptions">Client 配置类型</typeparam>
        /// <param name="services"></param>
        /// <param name="optionConfig">client 配置获取函数</param>
        /// <returns></returns>
        public static IServiceCollection AddLiteIMStackExchangeRedis<TImClientOptions>(this IServiceCollection services, Func<IServiceProvider, TImClientOptions> optionConfig)
            where TImClientOptions : class, IStackExchangeRedisImClientOptions
        {
            services.TryAddSingleton<IStackExchangeRedisImClientOptions>(optionConfig);

            services.AddLiteIMCore<StackExchangeRedisImClient, TImClientOptions>(optionConfig);

            return services;
        }
    }
}