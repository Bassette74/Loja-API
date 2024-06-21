using loja.Data;
using Microsoft.EntityFrameworkCore;

public class VendaService
{
    private readonly LojaDbContext _dbContext;

    public VendaService(LojaDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<int> GravarVendaAsync(Venda venda)
    {
        // Verifica se o cliente existe
        var cliente = await _dbContext.Clientes.FindAsync(venda.ClienteId);
        if (cliente == null)
        {
            throw new ArgumentException($"Cliente com ID {venda.ClienteId} não encontrado.");
        }

        // Verifica se o produto existe
        var produto = await _dbContext.Produtos.FindAsync(venda.ProdutoId);
        if (produto == null)
        {
            throw new ArgumentException($"Produto com ID {venda.ProdutoId} não encontrado.");
        }

        // Adiciona a venda ao contexto e salva no banco de dados
        _dbContext.Vendas.Add(venda);
        await _dbContext.SaveChangesAsync();

        return venda.Id;
    }

    public async Task<List<object>> ConsultarVendasPorProdutoDetalhadaAsync(int produtoId)
    {
        var vendasDetalhadas = await _dbContext.Vendas
            .Where(v => v.ProdutoId == produtoId)
            .Select(v => new
            {
                ProdutoNome = v.Produto.nome,
                DataVenda = v.DataVenda,
                VendaId = v.Id,
                ClienteNome = v.Cliente.name,
                QuantidadeVendida = v.QuantidadeVendida,
                PrecoVenda = v.PrecoUnitario
            })
            .ToListAsync();

        return vendasDetalhadas.Select(v => (object)v).ToList();
    }

    public async Task<List<object>> ConsultarVendasPorProdutoSumarizadaAsync(int produtoId)
    {
        var vendasSumarizadas = await _dbContext.Vendas
            .Where(v => v.ProdutoId == produtoId)
            .GroupBy(v => v.ProdutoId)
            .Select(g => new
            {
                ProdutoNome = g.First().Produto.nome,
                TotalQuantidadeVendida = g.Sum(v => v.QuantidadeVendida),
                TotalPrecoVenda = g.Sum(v => v.PrecoUnitario)
            })
            .ToListAsync();

        return vendasSumarizadas.Select(v => (object)v).ToList();
    }

    public async Task<List<object>> ConsultarVendasPorClienteDetalhadaAsync(int clienteId)
    {
        var vendasDetalhadas = await _dbContext.Vendas
            .Where(v => v.ClienteId == clienteId)
            .Select(v => new
            {
                ProdutoNome = v.Produto.nome,
                DataVenda = v.DataVenda,
                VendaId = v.Id,
                QuantidadeVendida = v.QuantidadeVendida,
                PrecoVenda = v.PrecoUnitario
            })
            .ToListAsync();

        return vendasDetalhadas.Select(v => (object)v).ToList();
    }

    public async Task<List<object>> ConsultarVendasPorClienteSumarizadaAsync(int clienteId)
    {
        var vendasSumarizadas = await _dbContext.Vendas
            .Where(v => v.ClienteId == clienteId)
            .GroupBy(v => v.ClienteId)
            .Select(g => new
            {
                TotalPrecoVenda = g.Sum(v => v.PrecoUnitario),
                ProdutosVendidos = g.Select(v => new { ProdutoNome = v.Produto.nome, QuantidadeVendida = v.QuantidadeVendida })
            })
            .ToListAsync();

        return vendasSumarizadas.Select(v => (object)v).ToList();
    }
}
