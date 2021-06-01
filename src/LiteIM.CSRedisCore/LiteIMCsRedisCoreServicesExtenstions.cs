using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

using System;
using System.Collections.Generic;
using System.Text;

namespace LiteIM
{
    public static class LiteIMCsRedisCoreServicesExtenstions
    {
        /// <summary>
        /// 添加 LiteIM CsRedis 到依赖注入容器
        /// </summary>
        /// <typeparam name="TImClient">Client类型</typeparam>
        /// <typeparam name="TImClientOptions">Client配置类型</typeparam>
        /// <param name="services"></param>
        /// <param name="options">client 配置</param>
        /// <returns></returns>
        public static IServiceCollection AddLiteIMCsRedis<TImClient, TImClientOptions>(this IServiceCollection services, TImClientOptions options)
            where TImClient : class, IImClient, new()
            where TImClientOptions : class, ICsRedisCoreImClientOptions, new()
        {
            services.AddLiteIMCsRedis<TImClient, TImClientOptions>((s) =>
            {
                return options;
            });

            return services;
        }

        /// <summary>
        /// 添加 LiteIM CsRedis 到依赖注入容器
        /// </summary>
        /// <typeparam name="TImClient">Client类型</typeparam>
        /// <typeparam name="TImClientOptions">Client配置类型</typeparam>
        /// <param name="services"></param>
        /// <param name="optionConfig">client 配置获取函数</param>
        /// <returns></returns>
        public static IServiceCollection AddLiteIMCsRedis<TImClient, TImClientOptions>(this IServiceCollection services, Func<IServiceProvider, TImClientOptions> optionConfig)
            where TImClient : class, IImClient, new()
            where TImClientOptions : class, ICsRedisCoreImClientOptions, new()
        {
            services.TryAddSingleton<ICsRedisCoreImClientOptions>(optionConfig);

            services.AddLiteIMCore<TImClient, TImClientOptions>(optionConfig);

            return services;
        }
    }
}
