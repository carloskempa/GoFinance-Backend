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
            //Context
            services.AddScoped<FinanceContext>();

            // Notifications
            services.AddScoped<INotificationHandler<DomainNotification>, DomainNotificationHandler>();

            //Repositories
            services.AddScoped<IContaFinanceiraRepository, ContaFinanceiraRepository>();
            services.AddScoped<IMovimentoRepository, MovimentoRepository>();
            services.AddScoped<ISmtpRepository, SmtpRepository>();
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();

            //Services
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<ICriptografiaService, CriptografiaService>();
        }
    }
}
