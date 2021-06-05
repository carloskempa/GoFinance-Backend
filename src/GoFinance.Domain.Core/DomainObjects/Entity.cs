using System;

namespace GoFinance.Domain.Core.DomainObjects
{
    public abstract class Entity
    {
        public Guid Id { get; set; }
        public DateTime DtCadastro { get; set; }
        public Entity()
        {
            Id = new Guid();
        }

        public virtual void Validar()
        {
            throw new NotImplementedException();
        }

    }
}
