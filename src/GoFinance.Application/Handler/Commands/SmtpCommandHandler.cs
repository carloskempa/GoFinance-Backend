using GoFinance.Application.Commands;
using GoFinance.Domain.Core.Communication.Mediator;
using GoFinance.Domain.Core.ValuesObjects;
using GoFinance.Domain.Entities;
using GoFinance.Domain.Interfaces.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace GoFinance.Application.Handler.Commands
{
    public class SmtpCommandHandler : HandlerBase, IRequestHandler<AdicionarSmtpCommand, bool>,
                                                   IRequestHandler<AtualizarSmtpCommand, bool>,
                                                   IRequestHandler<DeletarSmtpCommand, bool>
    {

        private readonly ISmtpRepository _smtpRepository;

        public SmtpCommandHandler(ISmtpRepository smtpRepository, IMediatorHandler mediatorHandler) : base(mediatorHandler)
        {
            _smtpRepository = smtpRepository;
        }

        public async Task<bool> Handle(AdicionarSmtpCommand request, CancellationToken cancellationToken)
        {
            if (!ValidarComando(request))
                return false;

            var smtp = new Smtp(new Email(request.Email), request.Mascara, request.Host, request.Porta, request.Usuario, request.Senha, request.SSL);
            _smtpRepository.Adicionar(smtp);

            return await Commit(_smtpRepository.UnitOfWork);
        }

        public async Task<bool> Handle(AtualizarSmtpCommand request, CancellationToken cancellationToken)
        {
            if (!ValidarComando(request))
                return false;

            var smtp =await _smtpRepository.ObterPorId(request.SmtpId);
            if(smtp == null)
            {
                await AdicionarEventError(request.MessageType, "Smtp não encontrado");
                return false;
            }

            smtp.Atualizar(new Email(request.Email), request.Mascara, request.Host, request.Porta, request.Usuario, request.Senha, request.SSL);
            _smtpRepository.Atualizar(smtp);

            return await Commit(_smtpRepository.UnitOfWork);
        }

        public async Task<bool> Handle(DeletarSmtpCommand request, CancellationToken cancellationToken)
        {
            if (!ValidarComando(request))
                return false;

            var smtp = await _smtpRepository.ObterPorId(request.SmtpId);
            _smtpRepository.Deletar(smtp);

            return await Commit(_smtpRepository.UnitOfWork);
        }
    }
}
