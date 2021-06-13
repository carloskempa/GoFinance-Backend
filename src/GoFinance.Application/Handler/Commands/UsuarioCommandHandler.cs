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
using System;
using System.Threading;
using System.Threading.Tasks;

namespace GoFinance.Application.Handler.Commands
{
    public class UsuarioCommandHandler : IRequestHandler<AdicionarUsuarioCommand, bool>,
                                         IRequestHandler<AuthenticarUsuarioCommand, bool>,
                                         IRequestHandler<SolicitarRecuperacaoSenhaUsuarioCommand, bool>,
                                         IRequestHandler<RecuperarSenhaUsuarioCommand, bool>,
                                         IRequestHandler<AtualizarUsuarioCommand, bool>
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

            var emailExisteCadastrado = await VerificarSeEmailJaExiste(request.Email);

            if (emailExisteCadastrado)
            {
                await _mediatorHandler.PublicarNotificacao(new DomainNotification(request.MessageType, "Email informado já cadastrado."));
                return false;
            }

            var loginExisteCadastrado = await VeridicarSeLoginJaExiste(request.Login);

            if (loginExisteCadastrado)
            {
                await _mediatorHandler.PublicarNotificacao(new DomainNotification(request.MessageType, "Login informado já cadastrado."));
                return false;
            }

            var senhaUsuario = _criptografiaService.Encrypt(request.Senha);
            var usuario = new Usuario(request.Nome, request.Login, senhaUsuario, new Email(request.Email), true, request.Administrador);

            _usuarioRepository.Adicionar(usuario);

            return await _usuarioRepository.UnitOfWork.Commit();
        }

        public async Task<bool> Handle(AuthenticarUsuarioCommand request, CancellationToken cancellationToken)
        {
            if (!ValidarComando(request))
                return false;

            var usuario = await _usuarioRepository.ObterPorLogin(request.Login);

            if (usuario == null)
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

            usuario.CriarRefreshToken();
            _usuarioRepository.Atualizar(usuario);

            return await _usuarioRepository.UnitOfWork.Commit();
        }

        public async Task<bool> Handle(SolicitarRecuperacaoSenhaUsuarioCommand request, CancellationToken cancellationToken)
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

            usuario.AdicionarEvento(new EnviarEmailResetarSenhaUsuarioEvent(usuario, request.UrlSite));

            return true;
        }

        public async Task<bool> Handle(RecuperarSenhaUsuarioCommand request, CancellationToken cancellationToken)
        {
            if (!ValidarComando(request))
                return false;

            var usuario = await _usuarioRepository.ObterPorTokenAlterarSenha(request.TokenAlterarSenha);

            if (usuario == null)
            {
                await _mediatorHandler.PublicarNotificacao(new DomainNotification(request.MessageType, "Token informado é inválido"));
                return false;
            }

            if (usuario.ValidarDataAlteracaoSenhaEstaExpirado())
            {
                await _mediatorHandler.PublicarNotificacao(new DomainNotification(request.MessageType, "Token informado já expirou, solicite novamente em recuperar senha."));
                return false;
            }

            var novaSenhaUsuario = _criptografiaService.Encrypt(request.Senha);
            usuario.AlterarSenha(novaSenhaUsuario);
            _usuarioRepository.Atualizar(usuario);

            return await _usuarioRepository.UnitOfWork.Commit();
        }

        public async Task<bool> Handle(AtualizarUsuarioCommand request, CancellationToken cancellationToken)
        {
            if (!ValidarComando(request))
                return false;

            var usuario = await _usuarioRepository.ObterPorId(request.UsuarioId);

            if (usuario == null)
            {
                await _mediatorHandler.PublicarNotificacao(new DomainNotification(request.MessageType, "Usuário não encontrado"));
                return false;
            }

            var emailExisteCadastrado = await VerificarSeEmailJaExiste(request.Email, request.UsuarioId);

            if (emailExisteCadastrado)
            {
                await _mediatorHandler.PublicarNotificacao(new DomainNotification(request.MessageType, "Email informado já cadastrado."));
                return false;
            }

            var loginExisteCadastrado = await VeridicarSeLoginJaExiste(request.Login, request.UsuarioId);

            if (loginExisteCadastrado)
            {
                await _mediatorHandler.PublicarNotificacao(new DomainNotification(request.MessageType, "Login informado já cadastrado."));
                return false;
            }

            usuario.Atualizar(request.Nome, request.Login, new Email(request.Email), request.Ativo, request.Administrador);
            _usuarioRepository.Atualizar(usuario);

            return await _usuarioRepository.UnitOfWork.Commit();
        }

        private async Task<bool> VerificarSeEmailJaExiste(string email, Guid usuarioId)
        {
            var usuario = await _usuarioRepository.ObterPorEmail(email);

            if (usuario != null)
            {
                if (usuario.Id == usuarioId)
                    return false;

                return true;
            }

            return false;
        }

        private async Task<bool> VerificarSeEmailJaExiste(string email)
        {
            var usuario = await _usuarioRepository.ObterPorEmail(email);
            return usuario != null;
        }

        private async Task<bool> VeridicarSeLoginJaExiste(string login, Guid usuarioId)
        {
            var usuario = await _usuarioRepository.ObterPorLogin(login);

            if (usuario == null)
                return false;

            if (usuario.Id == usuarioId)
                return false;

            return true;
        }

        private async Task<bool> VeridicarSeLoginJaExiste(string login)
        {
            var usuario = await _usuarioRepository.ObterPorLogin(login);
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
