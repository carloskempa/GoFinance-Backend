using GoFinance.Application.Events;
using GoFinance.Domain.Interfaces.Repositories;
using GoFinance.Domain.Interfaces.Services;
using GoFinances.Infra.Helpers;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace GoFinance.Application.Handler.Event
{
    public class UsuarioEventHandler : INotificationHandler<EnviarEmailResetarSenhaUsuarioEvent>
    {
        private readonly IEmailService _emailService;
        private readonly ISmtpRepository _smtpRepository;

        public UsuarioEventHandler(IEmailService emailService, ISmtpRepository smtpRepository)
        {
            _emailService = emailService;
            _smtpRepository = smtpRepository;
        }

        public async Task Handle(EnviarEmailResetarSenhaUsuarioEvent notification, CancellationToken cancellationToken)
        {
            var smtp = await _smtpRepository.ObterAtivo();
            var htmlBody = TemplatesEmail.ObterHtmlResetarSenhaUsuario(notification.Usuario, notification.UrlSite);

            await _emailService.Enviar(smtp, notification.Usuario.Email.Endereco, "Recuperação de Senha", htmlBody);
        }
    }
}
