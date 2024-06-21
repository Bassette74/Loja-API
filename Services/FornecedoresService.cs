// Implemente a classe ProductService:
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Loja.Models;
using Loja;
using loja.Data;

namespace loja.services
{
    public class FornecedoresService
    {
        private readonly LojaDbContext _dbContext;

        public FornecedoresService(LojaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // Método para consultar todos os produtos
        public async Task<List<Fornecedores>> GetAllProductsAsync()
        {
            return await _dbContext.Fornecedores.ToListAsync();
        }

        // Métodd para consultar um produto a partir do seu Id
        public async Task<Fornecedores> GetProductByIdAsync(int id)
        {
            return await _dbContext.Fornecedores.FindAsync(id);
        }

        // Método para  gravar um novo produto
        public async Task AddProductAsync(Fornecedores fornecedores)
        {
            _dbContext.Fornecedores.Add(fornecedores);
            await _dbContext.SaveChangesAsync();
        }

        // Método para atualizar os dados de um produto
        public async Task UpdateProductAsync(Fornecedores fornecedores)
        {
            _dbContext.Entry(fornecedores).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        // Método para excluir um produto
        public async Task DeleteProductAsync(int id)
        {
            var fornecedores = await _dbContext.Fornecedores.FindAsync(id);
            if (fornecedores != null)
            {
                _dbContext. Fornecedores.Remove(fornecedores);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
