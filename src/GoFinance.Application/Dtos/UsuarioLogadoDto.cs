using System;

namespace GoFinance.Application.Dtos
{
    public class UsuarioLogadoDto
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Usuario { get; set; }
        public string Perfil { get; set; }
        public string Email { get; set; }
        public TokenDto Auth { get; set; }
    }

    public class TokenDto
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
    }
}
