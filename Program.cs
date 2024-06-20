/*
using loja.Data;
using loja.Data.loja.data;
using Loja;
using Loja.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Microsoft.EntityFrameworkCore.Update.Internal;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configurando a conexão com o banco de dados
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<LojaDbContext>(options =>
    options.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 26))));


var app = builder.Build();



app.UseHttpsRedirection();

//Cadastrar Produtos --------------------------------------------------------------------------------------------------------

app.MapPost("/createProduto" , async (LojaDbContext DbContext , Produto newProduto)=>{
    DbContext.Produtos.Add(newProduto);
    await DbContext.SaveChangesAsync();
    return Results.Created($"/createProduto/{newProduto.Id}", newProduto);
});
 // Consultar dados do DB
 app.MapGet("/produtos", async (LojaDbContext DbContext) =>{
    var produtos = await DbContext.Produtos.ToListAsync();
    return Results.Ok(produtos);

 });

//Atualiza Produto Existente
app.MapPut("/Produto/{id}", async (int id , LojaDbContext dbContext , Produto updatedProduto)=>{

    //verifica Produto existente na base , conforme o id informado
    //se o produto existir na base , sera retornado  para dentro do objeto existingProduto

    var existingProduto = await dbContext.Produtos.FindAsync(id);
    if(existingProduto is null){
        return Results.NotFound($"Produto with ID {id} not found.");

}

existingProduto.nome = updatedProduto.nome;
existingProduto.preco = updatedProduto.preco;
existingProduto.fornecedor = updatedProduto.fornecedor;

//SALVA NO DB
await dbContext.SaveChangesAsync();

//retorna para o cliente que invocou o endpoint
return Results.Ok(existingProduto);

});

//endpoint para criar cliente -------------------------------------------------------------------------------------------------

app.MapPost("/createcliente" , async (LojaDbContext DbContext , Cliente newCliente)=>{
    DbContext.Clientes.Add(newCliente);
    await DbContext.SaveChangesAsync();
    return Results.Created($"/createcliente/{newCliente.Id}", newCliente);
});
  //consulta no DB
app.MapGet("/cliente", async (LojaDbContext DbContext) =>{
    var cliente = await DbContext.Clientes.ToListAsync();
    return Results.Ok(cliente);

 });

//Atualiza Produto Existente
app.MapPut("/cliente/{id}", async (int id , LojaDbContext dbContext , Cliente updatedCliente)=>{

    //verifica Produto existente na base , conforme o id informado
    //se o produto existir na base , sera retornado  para dentro do objeto existingProduto

    var existingCliente = await dbContext.Clientes.FindAsync(id);
    if(existingCliente is null){
        return Results.NotFound($"Produto with ID {id} not found.");

}

    existingCliente.name = updatedCliente.name;
    existingCliente.email = updatedCliente.email;
    existingCliente.cpf = updatedCliente.cpf;

//SALVA NO DB
await dbContext.SaveChangesAsync();

//retorna para o cliente que invocou o endpoint
return Results.Ok(existingCliente);

});


//Cretate Fornecedor ---------------------------------------------------------------------------------------------------------------

app.MapPost("/createfotnecedor" , async (LojaDbContext DbContext , Fornecedores newFornecedor)=>{
    DbContext.Fornecedores.Add(newFornecedor);
    await DbContext.SaveChangesAsync();
    return Results.Created($"/createfornecedor/{newFornecedor.id}", newFornecedor);
});
  //consulta no DB
app.MapGet("/fornecedor", async (LojaDbContext DbContext) =>{
    var fornecedores = await DbContext.Fornecedores.ToListAsync();
    return Results.Ok(fornecedores);

 });

//Atualiza Produto Existente
app.MapPut("/fornecedores/{id}", async (int id , LojaDbContext dbContext , Fornecedores updatedfornecedor)=>{

    //verifica Produto existente na base , conforme o id informado
    //se o produto existir na base , sera retornado  para dentro do objeto existingProduto

    var existingFornecedor = await dbContext.Fornecedores.FindAsync(id);
    if(existingFornecedor is null){
        return Results.NotFound($"Produto with ID {id} not found.");

}

    existingFornecedor.nome = updatedfornecedor.nome;
    existingFornecedor.email = updatedfornecedor.email;
    existingFornecedor.cnpj = updatedfornecedor.cnpj;

//SALVA NO DB
await dbContext.SaveChangesAsync();

//retorna para o cliente que invocou o endpoint
return Results.Ok(existingFornecedor);

});

*/

using Microsoft.AspNetCore.Mvc;

using loja.services;
using System.Collections.Generic;
using System.Threading.Tasks;
using Loja.Models;
using Loja;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<ProductService>();

var app = builder.Build();

// Configurar as requisições HTTP 
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

// ---------------ENDPOINTS PRODUTOS ---------------------------------------------------------------------

app.MapGet("/produtos", async (ProductService productService) =>
{
    var produtos = await productService.GetAllProductsAsync();
    return Results.Ok(produtos);
});

app.MapGet("/produtos/{id}", async (int id, ProductService productService) =>
{
    var produto = await productService.GetProductByIdAsync(id);
    if (produto == null)
    {
        return Results.NotFound($"Product with ID {id} not found.");
    }
    return Results.Ok(produto);
});

app.MapPost("/produtos", async (Produto produto, ProductService productService) =>
{
    await productService.AddProductAsync(produto);
    return Results.Created($"/produtos/{produto.Id}", produto);

});

app.MapPut("/produtos/{id}", async (int id, Produto produto, ProductService productService) =>
{
    if (id != produto.Id)
    {
        return Results.BadRequest("Product ID mismatch.");
    }

    await productService.UpdateProductAsync(produto);
    return Results.Ok();
});

app.MapDelete("/produtos/{id}", async (int id, ProductService productService) =>
{
    await productService.DeleteProductAsync(id);
    return Results.Ok();
});

app.Run();

// --------------------ENDPOINTS CLIENTES --------------------------------------------------------

app.MapGet("/cliente", async (ClienteService clienteService) =>
{
    var clientes = await clienteService.GetAllProductsAsync();
    return Results.Ok(clientes);
});

app.MapGet("/cliente/{id}", async (int id, ClienteService clienteService) =>
{
    var cliente = await clienteService.GetProductByIdAsync(id);
    if (cliente == null)
    {
        return Results.NotFound($"Cliente with ID {id} not found.");
    }
    return Results.Ok(cliente);
});

app.MapPost("/cliente", async (Cliente cliente, ClienteService clienteService) =>
{
    await clienteService.AddProductAsync(cliente);
    return Results.Created($"/cliente/{cliente.Id}", cliente);

});

app.MapPut("/cliente/{id}", async (int id, Cliente cliente, ClienteService clienteService) =>
{
    if (id != cliente.Id)
    {
        return Results.BadRequest("Cliente ID mismatch.");
    }

    await clienteService.UpdateProductAsync(cliente);
    return Results.Ok();
});

app.MapDelete("/cliente/{id}", async (int id, ClienteService clienteService) =>
{
    await clienteService.DeleteProductAsync(id);
    return Results.Ok();
});


//---------------Endpoint Fornecedores-----------------------------------------------------------------------------







