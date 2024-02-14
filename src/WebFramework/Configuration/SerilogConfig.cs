using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;
using System;

public static class SerilogConfig
{
    public static Action<HostBuilderContext, LoggerConfiguration> ConfigureLogger =>
          (hostingContext, loggerConfiguration) =>
          {

              var env = hostingContext.HostingEnvironment;
              var applicationName = env.ApplicationName;
              var environmentName = env.EnvironmentName;


              loggerConfiguration.MinimumLevel.Information()
                  .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                  .MinimumLevel.Override("System.Net.Http.HttpClient", LogEventLevel.Warning)
                  .MinimumLevel.Override("Microsoft.Hosting.Lifetime", LogEventLevel.Information)
                  .Enrich.WithProperty("ApplicationName", applicationName)
                  .Enrich.WithProperty("EnvironmentName", environmentName)
                  //.Enrich.With<ActivityEnricher>()
                  .WriteTo.Console();

              //var elasticUrl = "http://localhost:9200";

              //if (!string.IsNullOrEmpty(elasticUrl))
              //{
              //   var indexFormat = hostingContext.Configuration["IndexFormat"];

              //    loggerConfiguration.WriteTo.Elasticsearch(
              //        new ElasticsearchSinkOptions(new Uri(elasticUrl))
              //        {
              //            AutoRegisterTemplate = true,
              //            AutoRegisterTemplateVersion = AutoRegisterTemplateVersion.ESv7,
              //            IndexFormat = indexFormat,
              //            MinimumLogEventLevel = LogEventLevel.Debug
              //        });
              //}

              var seqUrl = hostingContext.Configuration["SeqOption:Url"];

              if (!string.IsNullOrEmpty(seqUrl))
              {
                  loggerConfiguration.WriteTo.Seq(seqUrl);
              }
          };
}


