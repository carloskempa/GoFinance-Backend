using GoFinance.Application.Commands;
using GoFinance.Application.Handler.Commands;
using GoFinance.Data.Context;
using GoFinance.Data.Repository;
using GoFinance.Domain.Core.Messages.CommonMessages.Notifications;
using GoFinance.Domain.Interfaces.Repositories;
using GoFinance.Domain.Interfaces.Services;
using GoFinance.Domain.Services;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace GoFinance.Infra.IoC
{
    public static class DependencyInjection
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<FinanceContext>();
            services.AddScoped<INotificationHandler<DomainNotification>, DomainNotificationHandler>();

            AddRepositories(services);
            AddServices(services);
            AddHandlers(services);
        }



        public static void AddRepositories(IServiceCollection services)
        {
            services.AddScoped<IContaFinanceiraRepository, ContaFinanceiraRepository>();
            services.AddScoped<IMovimentoRepository, MovimentoRepository>();
            services.AddScoped<ISmtpRepository, SmtpRepository>();
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
        }

        public static void AddServices(IServiceCollection services)
        {
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<ICriptografiaService, CriptografiaService>();
        }

        public static void AddHandlers(IServiceCollection services)
        {
            services.AddScoped<IRequestHandler<AuthenticarUsuarioCommand, bool>, UsuarioCommandHandler>();
            services.AddScoped<IRequestHandler<AdicionarUsuarioCommand, bool>, UsuarioCommandHandler>();

        }
    }
}
