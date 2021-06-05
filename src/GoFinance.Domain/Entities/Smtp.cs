using GoFinance.Domain.Core.DomainObjects;
using GoFinance.Domain.Core.ValuesObjects;

namespace GoFinance.Domain.Entities
{
    public class Smtp : Entity, IAggregateRoot
    {
        public Smtp(Email email, string mascara, string host, int porta, string usuario, string senha, bool ssl)
        {
            Email = email;
            Mascara = mascara;
            Host = host;
            Porta = porta;
            Usuario = usuario;
            Senha = senha;
            SSL = ssl;
        }

        protected Smtp() { }

        public Email Email { get; private set; }
        public string Mascara { get; private set; }
        public string Host { get; private set; }
        public int Porta { get; private set; }
        public string Usuario { get; private set; }
        public string Senha { get; private set; }
        public bool SSL { get; private set; }
        public bool Ativo { get; private set; }

        public void Ativar() => Ativo = true;
        public void Desativar() => Ativo = false;

        public override void Validar()
        {
            Validacoes.ValidarSeVazio(Mascara, "O campo máscara não pode ser vazio.");
            Validacoes.ValidarSeVazio(Host, "O campo Host não pode ser vazio.");
            Validacoes.ValidarSeVazio(Usuario, "O campo usuário não pode ser vazio.");
            Validacoes.ValidarSeVazio(Senha, "O campo senha não pode ser vazio.");
            Validacoes.ValidarSeIgual(Porta, 0, "O campo Porta não pode ser 0.");
        }

    }
}