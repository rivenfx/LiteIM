using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using StackExchange.Redis;

using SampleWebApp.MessageHub;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using LiteIM;

namespace SampleWebApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            services.AddSignalR();

            // 示例值：同步兼容路径较多时，适当提高线程池最小线程数，避免突发请求下扩容滞后。
            ThreadPool.SetMinThreads(
                Configuration.GetValue<int?>("LiteIM:ThreadPool:MinWorkerThreads") ?? 200,
                Configuration.GetValue<int?>("LiteIM:ThreadPool:MinCompletionPortThreads") ?? 200);

            // 添加 im StackExchange.Redis 的服务
            var options = new StackExchangeRedisImClientOptions()
            {
                Redis = ConnectionMultiplexer.Connect(
                    Configuration["LiteIM:Redis:ConnectionString"]
                    ?? "127.0.0.1:6379,abortConnect=false,connectTimeout=5000,syncTimeout=5000,asyncTimeout=5000,connectRetry=3,keepAlive=60")
            };
            services.AddLiteIMStackExchangeRedis((s) =>
            {
                return options;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                endpoints.MapHub<ChatHub>("/chatHub");
            });
        }
    }
}
