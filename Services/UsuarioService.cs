using loja.Data;

using Loja.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Loja.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly LojaDbContext _dbContext;

        public UsuarioService(LojaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Usuario> AuthenticateAsync(string email, string senha)
        {
            // Implemente a lógica de autenticação aqui
            var usuario = await _dbContext.Usuarios.FirstOrDefaultAsync(u => u.Email == email && u.Senha == senha);
            return usuario;
        }

        public async Task<int> AddUsuarioAsync(Usuario usuario)
        {
            _dbContext.Usuarios.Add(usuario);
            await _dbContext.SaveChangesAsync();
            return usuario.Id;
        }

        public async Task<Usuario> GetUsuarioByIdAsync(int id)
        {
            return await _dbContext.Usuarios.FindAsync(id);
        }

        public async Task<List<Usuario>> GetAllUsuariosAsync()
        {
            return await _dbContext.Usuarios.ToListAsync();
        }
    }
}
