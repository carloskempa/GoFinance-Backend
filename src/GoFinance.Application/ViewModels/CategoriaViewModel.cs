using System;

namespace GoFinance.Application.ViewModels
{
    public class CategoriaViewModel
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public int Codigo { get; set; }
        public bool Ativo { get; set; }
        public Guid UsuarioId { get; set; }
    }
}
