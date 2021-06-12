using GoFinance.Application.Commands;
using GoFinance.Application.Events;
using GoFinance.Domain.Core.Communication.Mediator;
using GoFinance.Domain.Core.Messages;
using GoFinance.Domain.Core.Messages.CommonMessages.Notifications;
using GoFinance.Domain.Core.ValuesObjects;
using GoFinance.Domain.Entities;
using GoFinance.Domain.Interfaces.Repositories;
using GoFinance.Domain.Interfaces.Services;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace GoFinance.Application.Handler.Commands
{
    public class UsuarioCommandHandler : IRequestHandler<AdicionarUsuarioCommand, bool>,
                                         IRequestHandler<AuthenticarUsuarioCommand, bool>,
                                         IRequestHandler<ResetarSenhaUsuarioCommand, bool>
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IMediatorHandler _mediatorHandler;
        private readonly ICriptografiaService _criptografiaService;

        public UsuarioCommandHandler(IUsuarioRepository usuarioRepository,
                                     IMediatorHandler mediatorHandler,
                                     ICriptografiaService criptografiaService)
        {
            _usuarioRepository = usuarioRepository;
            _mediatorHandler = mediatorHandler;
            _criptografiaService = criptografiaService;
        }


        public async Task<bool> Handle(AdicionarUsuarioCommand request, CancellationToken cancellationToken)
        {
            if (!ValidarComando(request))
                return false;

            if (await VerificarSeEmailJaExiste(request.Email))
            {
                await _mediatorHandler.PublicarNotificacao(new DomainNotification(request.MessageType, "Email informado já existe."));
                return false;
            }

            var senhaUsuario = _criptografiaService.Encrypt(request.Senha);
            var usuario = new Usuario(request.Nome, request.Login, senhaUsuario, new Email(request.Email), true, request.Perfil);

            _usuarioRepository.Adicionar(usuario);

            return await _usuarioRepository.UnitOfWork.Commit();
        }

        public async Task<bool> Handle(AuthenticarUsuarioCommand request, CancellationToken cancellationToken)
        {
            if (!ValidarComando(request))
                return false;

            var usuario = await _usuarioRepository.ObterPorLogin(request.Login);

            if(usuario == null)
            {
                await _mediatorHandler.PublicarNotificacao(new DomainNotification(request.MessageType, "Usuário e senha inválidos."));
                return false;
            }

            var senhaUsuario = _criptografiaService.Encrypt(request.Senha);

            if (!usuario.Senha.Equals(senhaUsuario))
            {
                await _mediatorHandler.PublicarNotificacao(new DomainNotification(request.MessageType, "Usuário e senha inválidos."));
                return false;
            }

            return true;
        }

        public async Task<bool> Handle(ResetarSenhaUsuarioCommand request, CancellationToken cancellationToken)
        {
            if (!ValidarComando(request))
                return false;

            var usuario = await _usuarioRepository.ObterPorEmail(request.Email);

            if (usuario == null)
            {
                await _mediatorHandler.PublicarNotificacao(new DomainNotification(request.MessageType, "Usuário não encontrado com e-mail informado"));
                return false;
            }

            usuario.CriarTokenAlteracaoSenha();

            _usuarioRepository.Atualizar(usuario);
            await _usuarioRepository.UnitOfWork.Commit();

            usuario.AdicionarEvento(new EnviarEmailResetarSenhaUsuarioEvent(usuario.Id, request.));

            return true;
        }




        private async Task<bool> VerificarSeEmailJaExiste(string email)
        {
            var usuario = await _usuarioRepository.ObterPorEmail(email);
            return usuario != null;
        }

        private bool ValidarComando(Command message)
        {
            if (message.EhValido())
                return true;

            foreach (var error in message.ValidationResult.Errors)
            {
                _mediatorHandler.PublicarNotificacao(new DomainNotification(message.MessageType, error.ErrorMessage));
            }

            return false;
        }

        
    }
}
