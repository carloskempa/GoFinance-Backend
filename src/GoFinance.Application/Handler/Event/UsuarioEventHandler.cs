using GoFinance.Domain.Interfaces.Repositories;
using GoFinance.Domain.Interfaces.Services;
using GoFinances.Infra.Helpers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GoFinance.Application.Events
{
    public class UsuarioEventHandler : INotificationHandler<EnviarEmailResetarSenhaUsuarioEvent>
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IEmailService _emailService;
        private readonly ISmtpRepository _smtpRepository;

        public UsuarioEventHandler(IUsuarioRepository usuarioRepository, IEmailService emailService, ISmtpRepository smtpRepository)
        {
            _usuarioRepository = usuarioRepository;
            _emailService = emailService;
            _smtpRepository = smtpRepository;
        }

        public async Task Handle(EnviarEmailResetarSenhaUsuarioEvent notification, CancellationToken cancellationToken)
        {
            var usuario = await _usuarioRepository.ObterPorId(notification.UsuarioId);
            var smtp = await _smtpRepository.ObterAtivo();
            var htmlBody = TemplatesEmail.ObterHtmlResetarSenhaUsuario(usuario, notification.UrlSite);

            await _emailService.Enviar(smtp, usuario.Email.Endereco, "Recuperação de Senha", htmlBody);
        }
    }
}
