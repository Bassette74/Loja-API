using Loja.Models;
using System.Threading.Tasks;

namespace Loja.Services
{
    public interface IUsuarioService
    {
        Task<Usuario> AuthenticateAsync(string email, string senha);
        Task<int> AddUsuarioAsync(Usuario usuario);
        Task<Usuario> GetUsuarioByIdAsync(int id);
        Task<List<Usuario>> GetAllUsuariosAsync();
    }
}
