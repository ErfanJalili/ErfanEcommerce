using Hangfire;
using Hangfire.Annotations;
using Hangfire.Dashboard;
using Hangfire.Dashboard.BasicAuthorization;
using Hangfire.SqlServer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace WebFramework.Configuration
{
    public class AllowAnnoymusFilter : IDashboardAuthorizationFilter
    {
        public bool Authorize([NotNull] DashboardContext context)
        {
            return true;
        }
    }
    public static class HangefireServiceCollectionExtentions
    {
        public static IServiceCollection AddCustomHangFireServer(this IServiceCollection services, IConfiguration configuration)
        {
            // Add Hangfire services.
            services.AddHangfire(c => c
                .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings()
                .UseSqlServerStorage(configuration["HangfireOptions:DbConnection"], new SqlServerStorageOptions
                {
                    CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
                    SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
                    QueuePollInterval = TimeSpan.Zero,
                    UseRecommendedIsolationLevel = true,
                    DisableGlobalLocks = true
                }));

            // Add the processing server as IHostedService
            services.AddHangfireServer();

            return services;
        }

        public static IApplicationBuilder UseCustomHangfireDashbord(this IApplicationBuilder app, IConfiguration configuration)
        {
            app.UseHangfireDashboard("/hangfire", new DashboardOptions
            {
                Authorization = new IDashboardAuthorizationFilter[] { new AllowAnnoymusFilter() }
                //Authorization = new[] { new BasicAuthAuthorizationFilter(new BasicAuthAuthorizationFilterOptions
                //{
                //    RequireSsl = true,
                //    LoginCaseSensitive = true,
                //    Users = new []
                //    {
                //        new BasicAuthAuthorizationUser
                //        {
                //            Login = configuration["HangfireOptions:DashbordUserName"],
                //            PasswordClear = configuration["HangfireOptions:DashbordPassword"],
                //        }
                //    }

                //})}
            }); ;

            return app;
        }
    }
}
