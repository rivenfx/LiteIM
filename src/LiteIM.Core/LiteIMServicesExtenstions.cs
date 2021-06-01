using LiteIM;


using Microsoft.Extensions.DependencyInjection.Extensions;

using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class LiteIMServicesExtenstions
    {
        /// <summary>
        /// 添加 LiteIM 到依赖注入容器
        /// </summary>
        /// <typeparam name="TImClient">Client类型</typeparam>
        /// <typeparam name="TImClientOptions">Client配置类型</typeparam>
        /// <param name="services"></param>
        /// <param name="options">client 配置</param>
        /// <returns></returns>
        public static IServiceCollection AddLiteIMCore<TImClient, TImClientOptions>(this IServiceCollection services, TImClientOptions options)
            where TImClient : class, IImClient
            where TImClientOptions : class, IImClientOptions
        {
            services.AddLiteIMCore<TImClient, TImClientOptions>((s) =>
            {
                return options;
            });
            return services;
        }

        /// <summary>
        /// 添加 LiteIM 到依赖注入容器
        /// </summary>
        /// <typeparam name="TImClient">Client类型</typeparam>
        /// <typeparam name="TImClientOptions">Client配置类型</typeparam>
        /// <param name="services"></param>
        /// <param name="optionConfig">client 配置获取函数</param>
        /// <returns></returns>
        public static IServiceCollection AddLiteIMCore<TImClient, TImClientOptions>(this IServiceCollection services, Func<IServiceProvider, TImClientOptions> optionConfig)
           where TImClient : class, IImClient
           where TImClientOptions : class, IImClientOptions
        {
            services.TryAddSingleton<IImClientOptions>(optionConfig);
            services.TryAddSingleton<IImClient, TImClient>();

            return services;
        }
    }
}
