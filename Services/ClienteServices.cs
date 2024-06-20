// Implemente a classe ProductService:
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using loja.Data.loja.data;
using Loja.Models;
using Loja;

namespace loja.services
{
    public class ClienteService
    {
        private readonly LojaDbContext _dbContext;

        public ClienteService(LojaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // Método para consultar todos os produtos
        public async Task<List<Cliente>> GetAllProductsAsync()
        {
            return await _dbContext.Clientes.ToListAsync();
        }

        // Métodd para consultar um produto a partir do seu Id
        public async Task<Cliente> GetProductByIdAsync(int id)
        {
            return await _dbContext.Clientes.FindAsync(id);
        }

        // Método para  gravar um novo produto
        public async Task AddProductAsync(Cliente cliente)
        {
            _dbContext.Clientes.Add(cliente);
            await _dbContext.SaveChangesAsync();
        }

        // Método para atualizar os dados de um produto
        public async Task UpdateProductAsync(Cliente cliente)
        {
            _dbContext.Entry(cliente).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        // Método para excluir um produto
        public async Task DeleteProductAsync(int id)
        {
            var cliente = await _dbContext.Clientes.FindAsync(id);
            if (cliente != null)
            {
                _dbContext. Clientes.Remove(cliente);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
