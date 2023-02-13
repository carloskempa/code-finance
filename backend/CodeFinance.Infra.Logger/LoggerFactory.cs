using Serilog;
using Serilog.Sinks.Elasticsearch;
using System;

namespace CodeFinance.Infra.Logger
{
    public static class LoggerFactory
    {
        private static ILogger _logger;

        public static ILogger Create(LoggerConfig config)
        {
#if DEBUG
            CreateLogInConsole();
#endif
            CreateLogInElasticSeach(config);

            return _logger;
        }

        private static void CreateLogInConsole()
        {
            if (_logger == null)
            {
                _logger = Log.Logger = new LoggerConfiguration()
                          .MinimumLevel.Debug()
                          .WriteTo.Console()
                          .CreateLogger();
            }
        }

        private static void CreateLogInElasticSeach(LoggerConfig config)
        {
            if(_logger == null)
            {
                _logger = Log.Logger = new LoggerConfiguration()
                     .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri(config.Conexao))
                     {
                         AutoRegisterTemplate = true,
                         IndexFormat = config.Formato
                     }).CreateLogger();
            }
        }



    }
}
