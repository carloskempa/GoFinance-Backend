using GoFinance.Application.Validators;
using GoFinance.Domain.Core.Messages;
using System;

namespace GoFinance.Application.Commands
{
    public class AdicionarCategoriaCommand : Command
    {
        public AdicionarCategoriaCommand(string nome, int codigo, bool ativo, Guid usuarioId)
        {
            Nome = nome;
            Codigo = codigo;
            Ativo = ativo;
            UsuarioId = usuarioId;
        }

        public string Nome { get; private set; }
        public int Codigo { get; private set; }
        public bool Ativo { get; private set; }
        public Guid UsuarioId { get; private set; }

        public override bool EhValido()
        {
            ValidationResult = new AdicionarCategoriaValidator().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
