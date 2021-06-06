using System;
using System.Collections.Generic;
using GoFinance.Domain.Core.DomainObjects;
using GoFinance.Domain.Core.ValuesObjects;
using GoFinance.Domain.Enuns;

namespace GoFinance.Domain.Entities
{
    public class Usuario : Entity, IAggregateRoot
    {
        public Usuario(string nome, string login, byte[] senha, Email email, bool ativo, Perfil perfil)
        {
            Nome = nome;
            Login = login;
            Senha = senha;
            Email = email;
            Ativo = ativo;
            Perfil = perfil;

            Validar();
        }
        protected Usuario() { }

        public string Nome { get; private set; }
        public string Login { get; private set; }
        public byte[] Senha { get; private set; }
        public Email Email { get; private set; }
        public Perfil Perfil { get; set; }
        public string TokenAlteracaoSenha { get; private set; }
        public DateTime? DataExpiracaoToken { get; private set; }
        public string RefleshToken { get; private set; }
        public bool Ativo { get; private set; }


        //EF Relation
        public ICollection<Categoria> Categorias { get; private set; }
        public ICollection<Fornecedor> Fornecedores { get; private set; }
        public ICollection<ContaFinanceira> ContaFinanceiras { get; private set; }
        public ICollection<Movimento> Movimentacoes { get; private set; }
        public ICollection<ContasPagar> ContasPagar { get; private set; }


        public void Ativar() => Ativo = true;
        public void Desativar() => Ativo = false;
        public void AlterarSenha(byte[] senha)
        {
            Validacoes.ValidarSeNulo(Senha, "O campo Senha não pode estar vazio");
            this.Senha = senha;
        }
        public void ValidarSenhaAcesso(byte[] senha)
        {
            Validacoes.ValidarSeNulo(senha, "O campo Senha não pode estar vazio.");
            Validacoes.ValidarSeDiferente(Senha, senha, "Usuário e senha inválido.");
        }
        public void CriarTokenAlteracaoSenha()
        {
            TokenAlteracaoSenha = Guid.NewGuid().ToString().Replace("-", "");
            DataExpiracaoToken = DateTime.Now.AddHours(2);
        }
        public void ExpirarTokenAlterarSenha()
        {
            TokenAlteracaoSenha = null;
            DataExpiracaoToken = null;
        }
        public bool ValidarDataAlteracaoSenhaEstaExpirado()
        {
            if (DataExpiracaoToken.Value < DateTime.Now)
                return false;

            return true;
        }
        public void CriarRefreshToken() => RefleshToken = Guid.NewGuid().ToString().Replace("-", "");
        public override void Validar()
        {
            Validacoes.ValidarSeVazio(Nome, "O campo Nome não pode estar vazio");
            Validacoes.ValidarSeVazio(Login, "O campo Login não pode estar vazio");
            Validacoes.ValidarSeNulo(Senha, "O campo Senha não pode estar vazio");
            Validacoes.ValidarSeIgual(Perfil, 0, "O campo pérfil deve ser preenchido.");
        }
    }
}
