using GoFinance.Application.Commands;
using GoFinance.Application.Handler.Commands;
using GoFinance.Data.Context;
using GoFinance.Data.Repository;
using GoFinance.Domain.Core.Messages.CommonMessages.Notifications;
using GoFinance.Domain.Interfaces.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GoFinance.Service.Setup
{
    public static class NativeInjectorExtension
    {
        public static IServiceCollection AddDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            AddQueries(services);
            AddRepositories(services);
            AddHandlers(services);

            services.AddMediatR(typeof(Startup));
            services.AddDbContext<FinanceContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
            services.AddScoped<INotificationHandler<DomainNotification>, DomainNotificationHandler>();

            return services;
        }

        private static void AddQueries(IServiceCollection services)
        {
        }

        private static void AddRepositories(IServiceCollection services)
        {
            services.AddScoped<IContaFinanceiraRepository, ContaFinanceiraRepository>();
            services.AddScoped<IMovimentoRepository, MovimentoRepository>();
            services.AddScoped<ISmtpRepository, SmtpRepository>();
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
        }

        private static void AddHandlers(IServiceCollection services)
        {
            services.AddScoped<IRequestHandler<AdicionarCategoriaCommand, bool>, CategoriaCommadHandler>();
            services.AddScoped<IRequestHandler<AdicionarUsuarioCommand, bool>, UsuarioCommandHandler>();
            services.AddScoped<IRequestHandler<AtualizarCategoriaCommand, bool>, CategoriaCommadHandler>();
            services.AddScoped<IRequestHandler<AtualizarUsuarioCommand, bool>, UsuarioCommandHandler>();
            services.AddScoped<IRequestHandler<AuthenticarUsuarioCommand, bool>, UsuarioCommandHandler>();
            services.AddScoped<IRequestHandler<DeletarCategoriaCommad, bool>, CategoriaCommadHandler>();
            services.AddScoped<IRequestHandler<RecuperarSenhaUsuarioCommand, bool>, UsuarioCommandHandler>();
            services.AddScoped<IRequestHandler<SolicitarRecuperacaoSenhaUsuarioCommand, bool>, UsuarioCommandHandler>();
        }
    }
}
