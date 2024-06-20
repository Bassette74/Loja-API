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









