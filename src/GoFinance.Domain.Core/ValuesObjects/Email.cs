using GoFinance.Domain.Core.DomainObjects;

namespace GoFinance.Domain.Core.ValuesObjects
{
    public class Email
    {
        public Email(string endereco)
        {
            Endereco = endereco;
            Validar();
        }

        public string Endereco { get; private set; }

        private void Validar()
        {
            Validacoes.ValidarSeNulo(Endereco, "O campo e-mail deve ser preenchido");
            Validacoes.ValidarSeVazio(Endereco, "O campo e-mail deve ser preenchido");

            bool validEmail = false;
            int indexArr = Endereco.IndexOf('@');
            if (indexArr > 0)
            {
                int indexDot = Endereco.IndexOf('.', indexArr);
                if (indexDot - 1 > indexArr)
                {
                    if (indexDot + 1 < Endereco.Length)
                    {
                        string indexDot2 = Endereco.Substring(indexDot + 1, 1);
                        if (indexDot2 != ".")
                        {
                            validEmail = true;
                        }
                    }
                }
            }

            if (!validEmail)
                throw new DomainException("Endereço do e-mail é inválido");
        }
    }
}
