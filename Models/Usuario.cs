namespace Loja.Models
{
    public class Usuario
    {
        public int Id { get; set; } // Identificador único do usuário
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
    }
}
