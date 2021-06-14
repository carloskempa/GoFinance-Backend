using GoFinance.Domain.Core.Communication.Mediator;
using GoFinance.Domain.Core.Data;
using GoFinance.Domain.Core.Messages;
using GoFinance.Domain.Core.Messages.CommonMessages.Notifications;
using System.Threading.Tasks;

namespace GoFinance.Application.Handler
{
    public abstract class HandlerBase
    {
        private readonly IMediatorHandler _mediatorHandler;

        public HandlerBase(IMediatorHandler mediatorHandler)
        {
            _mediatorHandler = mediatorHandler;
        }

        protected async Task AdicionarEventError(string key, string value)
        {
            await _mediatorHandler.PublicarNotificacao(new DomainNotification(key, value));
        }

        protected bool ValidarComando(Command message)
        {
            if (message.EhValido())
                return true;

            foreach (var error in message.ValidationResult.Errors)
                _mediatorHandler.PublicarNotificacao(new DomainNotification(message.MessageType, error.ErrorMessage));

            return false;
        }

        protected async Task<bool> Commit(IUnitOfWork unitOfWork)
        {
            return await unitOfWork.Commit();
        }
    }
}
