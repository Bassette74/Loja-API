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
using System;
using System.IO;
using System.Text;
using System.Text.Json;

using Loja.Models;
using Loja.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using loja.Data;
using loja.services;
using Loja;

var builder = WebApplication.CreateBuilder(args);

// Adicionar o DbContext com MySQL ao contêiner de serviços
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<LojaDbContext>(options =>
    options.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 26))));

// Adicionar os serviços ao contêiner
builder.Services.AddScoped<ProductService>();
builder.Services.AddScoped<ClienteService>();
builder.Services.AddScoped<FornecedoresService>();
builder.Services.AddScoped<UsuarioService>();
builder.Services.AddSingleton(new JwtService("abcabcabcabcabcabcabcabcabcabcabc")); // Substitua pela sua chave secreta real

var app = builder.Build();

// Configuração das requisições HTTP
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

// --------------- ENDPOINTS PRODUTOS -----------------------------------------

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

// --------------- ENDPOINTS CLIENTES -----------------------------------------

app.MapGet("/clientes", async (ClienteService clienteService) =>
{
    var clientes = await clienteService.GetAllProductsAsync();
    return Results.Ok(clientes);
});

app.MapGet("/clientes/{id}", async (int id, ClienteService clienteService) =>
{
    var cliente = await clienteService.GetProductByIdAsync(id);
    if (cliente == null)
    {
        return Results.NotFound($"Cliente with ID {id} not found.");
    }
    return Results.Ok(cliente);
});

app.MapPost("/clientes", async (Cliente cliente, ClienteService clienteService) =>
{
    await clienteService.AddProductAsync(cliente);
    return Results.Created($"/clientes/{cliente.Id}", cliente);
});

app.MapPut("/clientes/{id}", async (int id, Cliente cliente, ClienteService clienteService) =>
{
    if (id != cliente.Id)
    {
        return Results.BadRequest("Cliente ID mismatch.");
    }

    await clienteService.UpdateProductAsync(cliente);
    return Results.Ok();
});

app.MapDelete("/clientes/{id}", async (int id, ClienteService clienteService) =>
{
    await clienteService.DeleteProductAsync(id);
    return Results.Ok();
});

// --------------- ENDPOINTS FORNECEDORES -------------------------------------

app.MapGet("/fornecedores", async (FornecedoresService fornecedoresService) =>
{
    var fornecedores = await fornecedoresService.GetAllProductsAsync();
    return Results.Ok(fornecedores);
});

app.MapGet("/fornecedores/{id}", async (int id, FornecedoresService fornecedoresService) =>
{
    var fornecedor = await fornecedoresService.GetProductByIdAsync(id);
    if (fornecedor == null)
    {
        return Results.NotFound($"Fornecedor with ID {id} not found.");
    }
    return Results.Ok(fornecedor);
});

app.MapPost("/fornecedores", async (Fornecedores fornecedores, FornecedoresService fornecedoresService) =>
{
    await fornecedoresService.AddProductAsync(fornecedores);
    return Results.Created($"/fornecedores/{fornecedores.id}", fornecedores);
});

app.MapPut("/fornecedores/{id}", async (int id, Fornecedores fornecedores, FornecedoresService fornecedoresService) =>
{
    if (id != fornecedores.id)
    {
        return Results.BadRequest("Fornecedor ID mismatch.");
    }

    await fornecedoresService.UpdateProductAsync(fornecedores);
    return Results.Ok();
});

app.MapDelete("/fornecedores/{id}", async (int id, FornecedoresService fornecedoresService) =>
{
    await fornecedoresService.DeleteProductAsync(id);
    return Results.Ok();
});

// --------------- ENDPOINTS USUÁRIOS -----------------------------------------

// Endpoint para criar um novo usuário
app.MapPost("/usuarios", async (HttpContext context, UsuarioService usuarioService, JwtService jwtService) =>
{
    // Receber o request
    using var reader = new StreamReader(context.Request.Body);
    var body = await reader.ReadToEndAsync();

    // Deserializar o objeto JSON do request
    var json = JsonDocument.Parse(body);
    var nome = json.RootElement.GetProperty("nome").GetString();
    var email = json.RootElement.GetProperty("email").GetString();
    var senha = json.RootElement.GetProperty("senha").GetString();

    // Criar novo usuário
    var novoUsuario = new Usuario { Nome = nome, Email = email, Senha = senha };
    int novoUsuarioId = await usuarioService.AddUsuarioAsync(novoUsuario);

    // Gerar token JWT para o novo usuário
    var token = jwtService.GenerateToken(email);

    // Retornar resposta com o token JWT e status Created
    var responseJson = new
    {
        token,
        novoUsuarioId
    };
    await context.Response.WriteAsJsonAsync(responseJson);
});

// Endpoint para autenticar usuário e gerar token JWT
app.MapPost("/login", async (HttpContext context, UsuarioService usuarioService, JwtService jwtService) =>
{
    // Receber o request
    using var reader = new StreamReader(context.Request.Body);
    var body = await reader.ReadToEndAsync();

    // Deserializar o objeto JSON do request
    var json = JsonDocument.Parse(body);
    var email = json.RootElement.GetProperty("email").GetString();
    var senha = json.RootElement.GetProperty("senha").GetString();

    try
    {
        // Verificar se as credenciais são válidas
        var usuario = await usuarioService.AuthenticateAsync(email, senha);
        if (usuario == null)
        {
            // Retornar erro de autenticação se não encontrar o usuário
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            await context.Response.WriteAsync("Credenciais inválidas");
            return;
        }

        // Gerar token JWT
        var token = jwtService.GenerateToken(email);

        // Retornar o token JWT
        await context.Response.WriteAsync(token);
    }
    catch (Exception ex)
    {
        // Em caso de exceção, retornar erro interno do servidor
        context.Response.StatusCode = StatusCodes.Status500InternalServerError;
        await context.Response.WriteAsync($"Erro ao processar requisição: {ex.Message}");
    }
});

// --------------- CONFIGURAÇÃO DO TOKEN JWT ----------------------------------

app.UseAuthentication();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("abcabcabcabcabcabcabcabcabcabcabc"))
        };
    });

// Rota segura: exemplo de rota que requer autenticação
app.MapGet("/rotaSegura", async (HttpContext context) =>
{
    // Verificar se o token está presente nos headers da requisição
    if (!context.Request.Headers.ContainsKey("Authorization"))
    {
        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
        await context.Response.WriteAsync("Token não fornecido");
        return;
    }

    // Obter o token do header Authorization
    var token = context.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

    // Validar o token JWT
    var jwtService = context.RequestServices.GetRequiredService<JwtService>();
    var principal = jwtService.ValidateToken(token);

    // Verificar se a validação do token foi bem-sucedida
    if (principal == null)
    {
        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
        await context.Response.WriteAsync("Token inválido");
        return;
    }

    // Se o token é válido: dar andamento na lógica do endpoint
    await context.Response.WriteAsync("Autorizado");
});

//---------------------------Aunteticaçõa em todas as rotas ---------------------------------
// Habilitar a autenticação JWT para proteger os endpoints
app.UseAuthentication();

// Configuração do pipeline de requisição
app.UseAuthorization();

// Configurar as requisições HTTP 
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}


app.MapWhen(
    context => context.Request.Path.StartsWithSegments("/api"),
    builder =>
    {
        builder.UseAuthentication();
        builder.UseAuthorization();

        // Define seus endpoints protegidos aqui
        app.MapGet("/api/produtos", async (ProductService productService) =>
        {
            var produtos = await productService.GetAllProductsAsync();
            return Results.Ok(produtos);
        });

        app.MapGet("/api/Cliente", async (ClienteService clienteService) =>
        {
            var clientes = await clienteService.GetAllProductsAsync();
            return Results.Ok(clientes);
        });
        app.MapGet("/api/fornecedores", async (FornecedoresService fornecedoresService) =>
        {
            var fornecedores = await fornecedoresService.GetAllProductsAsync();
            return Results.Ok(fornecedores);
        });
        
    });

app.Run();
