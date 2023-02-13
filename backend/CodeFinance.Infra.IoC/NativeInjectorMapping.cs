using CodeFinance.Application.App;
using CodeFinance.Application.AutoMapper;
using CodeFinance.Application.Commands;
using CodeFinance.Application.Events;
using CodeFinance.Application.Handlers.Commands;
using CodeFinance.Application.Handlers.Events;
using CodeFinance.Application.Interfaces.Application;
using CodeFinance.Data.Contexto;
using CodeFinance.Data.MongoDb;
using CodeFinance.Data.MongoDb.Config;
using CodeFinance.Domain.Core.Communication;
using CodeFinance.Domain.Core.Messages.CommonMessages;
using CodeFinance.Domain.Interfaces.Services;
using CodeFinance.Infra.CrossCutting.Models;
using CodeFinance.Infra.CrossCutting.Services;
using CodeFinance.Infra.Logger;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using static CodeFinance.Data.MongoDb.MongoDBContext;
using static CodeFinance.Infra.CrossCutting.Services.EmailService;

namespace CodeFinance.Infra.IoC
{
    public static class NativeInjectorMapping
    {
        public static void RegisterServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IMediatorHandler, MediatorHandler>();
            services.AddScoped<INotificationHandler<DomainNotification>, DomainNotificationHandler>();
            services.AddSingleton(AutoMapperConfiguration.RegisterMappings().CreateMapper());
            services.AddDbContext<CodeFinanceContext>(options => options.UseSqlServer(configuration.GetConnectionString("FinanceiroContext")));

            RegistrarLogging(services, configuration);
            RegistrarDbMongo(services, configuration);
            RegistrarServices(services, configuration);
            RegistrarCommand(services);
            RegistrarApps(services);
            RegistrarEvents(services);
        }

        private static void RegistrarServices(IServiceCollection services, IConfiguration configuration)
        {
            var smtpConfig = configuration.GetSection("Smtp").Get<SmtpConfig>();
            services.AddSingleton<IEmailService>(c => EmailServiceFactory.Create(smtpConfig));
            services.AddScoped<ICriptografiaService, CriptografiaService>();
        }

        private static void RegistrarEvents(IServiceCollection services)
        {
            services.AddScoped<INotificationHandler<UsuarioCadastradoEvent>, UsuarioEventHandler>();
            services.AddScoped<INotificationHandler<CategoriaCadastradoEvent>, CategoriaEventHandler>();
        }

        private static void RegistrarCommand(IServiceCollection services)
        {
            //Usuario
            services.AddScoped<IRequestHandler<CadastrarUsuarioCommand, bool>, UsuarioCommandHandler>();
            services.AddScoped<IRequestHandler<PublicarUsuarioFilaCommand, bool>, UsuarioCommandHandler>();

            //Categoria
            services.AddScoped<IRequestHandler<CadastrarCategoriaCommand, bool>, CategoriaCommandHandler>();
            services.AddScoped<IRequestHandler<PublicarCategoriaFilaCommand, bool>, CategoriaCommandHandler>();
        }

        private static void RegistrarApps(IServiceCollection services)
        {
            services.AddScoped<IUsuarioApp, UsuarioApp>();
            services.AddScoped<ICategoriaApp, CategoriaApp>();
        }

        private static void RegistrarLogging(IServiceCollection services, IConfiguration configuration)
        {
            var config = configuration.GetSection("Serilog").Get<LoggerConfig>();
            services.AddSingleton(LoggerFactory.Create(config));
        }

        private static void RegistrarDbMongo(IServiceCollection services, IConfiguration configuration)
        {
            var config = configuration.GetSection("MongoDb").Get<MongoDbSettings>();
            services.AddScoped<IMongoDBContext>(c => MongoDBContextFactory.Create(config));
        }
    }
}
