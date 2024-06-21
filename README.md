UNIVERSIDADE POSITIVO
TÓPICOS ESPECIAIS EM DESENVOLVIMENTO DE SISTEMAS
TUTORIAL ENTITYFRAMEWORK
Sumário
ETAPA1: CONFIGURANDO E CRIANDO A PRIMEIRA CLASSE MODELO DO PROJETO	1
ETAPA 2: ALTERANDO A ESTRUTURA DA MODEL PRODUTOS	6
ETAPA 3: INSERINDO, ALTERANDO E CONSULTANDO DADOS DOS PRODUTOS	7
ETAPA 4: PRATICANDO	11
DESAFIO	13
ETAPA5: IMPLEMENTANDO A CAMADA SERVICE	14
ETAPA6: PRATICANDO DA IMPLEMENTAÇÃO DE CLASSES SERVICE	17
ETAPA7: HABILITANDO AUTENTICAÇÃO NOS ENDPOINTS DO SISTEMA	17
ETAPA8: HABILITANDO AUTENTICAÇÃO NOS ENDPOINTS DO SISTEMA	19


Neste tutorial, você desenvolverá uma aplicação capaz de manter dados sobre produtos.
Importante: O MySql será usado com banco dedados
ETAPA1: CONFIGURANDO E CRIANDO A PRIMEIRA CLASSE MODELO DO PROJETO
1.	Crie o banco de dados para este tutorial usanto o MySql Workbench
a.	Será necessário criar um usuário específico para o novo sistema

b.	Comandos para criar o banco de dados e o usuário:
 
c.	Agora seu banco de dados possui um usuário que será utilizado pelo sistema para acessar as tabelas e demais operações de dados.

2.	Inicie um projeto do tipo webapi chamado Loja
3.	Entrar na pasta Loja (pasta criada para o seu projeto, conforme o item 1)
4.	Instale os pacotes do EntityFramework Core:
a.	dotnet tool install --global dotnet-ef
b.	dotnet add package Microsoft.EntityframeworkCore.Design
c.	dotnet add package Microsoft.EntityframeworkCore
d.	dotnet add package Pomelo.EntityframeworkCore.MySql

5.	Abra o VSCode
6.	Crie uma pasta models dentro do seu projeto
 
7.	Dentro da pasta models, implemente a classe Produto.cs
 
8.	Crie uma pasta (dentro do projeto) com o nome data
9.	Dentro da pasta data, implemente a classe LojaDbContext.cs
namespace loja.data{
    public class LojaDbContext : DbContext{
            public LojaDbContext(DoContextOptions<LojaDbContext> options) : base(options){}
            publicDbSet<ProducesResponseTypeMetadata> Produtos {get;set;}
    }
}

10.	Atualize o arquivo appsettings.json
 
a.	ATENÇÃO: Note que a conexão exemplo (acima) define o usuário do banco de dados como teds e senha como 12345678. Você deve criar um usuário próprio e alterar os dados diretamente na linha DefaultConnection.

 
11.	Dentro da pasta data, crie uma classe DbContextFactory para permitir que os comandos dotnet ef funcionem:
 
12.	Atualize o Program.cs, para configurar o acesso ao banco de dados, conforme os trechos destacados em vermelho
 
13.	Use o Terminal, e aplique as definições do banco de dados (o DotNet chama de Migrations)
a.	dotnet ef migrations add InitialCreate 
b.	dotnet ef database update
c.	dotnet run 
 
14.	Vá até o MySql Workbench e verifique o banco de dados teds (seu banco de dados pode  ter outro nome)
a.	Se tudo deu certo, o Entityframework criou uma pasta produtos no banco de dados
 
 
ETAPA 2: ALTERANDO A ESTRUTURA DA MODEL PRODUTOS
1.	Adicione a propriedade Fornecedor à classe Produto.cs
a.	Xx
2.	Atualize o banco de dados, criando uma nova Migration e atualizando o banco de dados
a.	dotnet ef migrations add AddFornecedorToProduto
b.	dotnet ef database update
3.	Verifique a tabela produtos no banco de dados, ela deve possuir uma nova coluna  Fornecedor
a.	 

 
ETAPA 3: INSERINDO, ALTERANDO E CONSULTANDO DADOS DOS PRODUTOS
INSERIR DADOS
1.	Implemente um endpoint para criar um novo produto na base de dados. Observe que o código do endpoint foi adicionado abaixo da linha app.UseHttpsRedirection();
 
2.	Rode o seu programa
a.	dotnet run
3.	Envie dados para a API usando o Postman
a.	Note que os dados enviados no payload do request obedecem exatamente os atributos da classe produto.
b.	Atenção para a porta usada no request. No exemplo abaixo, foi utilizada a porta 5276. Verifique em qual porta seu programa está rodando.
 
4.	Verifique o banco de dados
a.	Se o endpoint está funcionando corretamente e o request enviado está correto, os dados deve, estar gravados na tabela produtos



 
CONSULTAR DADOS
1.	Para consultar os dados de todos os produtos
a.	Implemente um endpoit GET que retorna todos os dados da classe Produto
 
b.	Teste o endpoint usando o Postman
 
2.	Para consultar um produto a partir de seu ID
a.	Implemente um endpoint do tipo GET que receba o ID e retorne os dados conforme o banco de dados
 
b.	Teste com o Postman
i.	A url do request (GET) será: localhost:5276/produtos/2
ii.	Atenção: O valor 2, após a barra [/2] representa um dos ids de produtos gravados na sua base de dados
iii.	Lembre-se de substituir a Porta 5276 pela correta (conforme sua aplicação)
 
 
ATUALIZANDO DADOS
3.	Para alterar os dados de um produto, faça:
a.	Implemente um enpoint do tipo PUT que receberá o ID e os novos dados do produto
 
b.	Teste o endpoint com o Postman
i.	Note que o tipo de requet é PUT
ii.	A url é localhost:5276/produtos/3
iii.	Atenção: O valor 3, após a barra [/3] representa um dos id do produto que será atualizado na sua base de dados
iv.	O body recebe os novos dados
 
v.	Verifique o banco de dados
  
ETAPA 4: PRATICANDO
1.	Implemente a classe Cliente no seu projeto
a.	Crie a classe Cliente.cs na pasta models
 
b.	Adicionar a classe cliente na DbContext.cs
 
c.	Adicionar a Migration (usando o terminal)
i.	dotnet ef migrations add AddCliente
ii.	dotnet ef database update
d.	Verifique a tabela criada no banco de dados
 
2.	Implemente o endpoint para criar um novo cliente
 
a.	Rode sua aplicação
i.	dotnet run

b.	Execute o endpoint usando o Postman
 
c.	Consulte a tabela clientes no banco de dados
 
3.	Implemente o endpoint para consultar todos os clientes do banco dedados
a.	Siga o exemplo criado para a classe Produto
4.	Implemente o endpoint para consultar um cliente a partir de seu ID
a.	Siga o exemplo criado para a classe Produto
5.	Implemente o endpint para altualizar os dados de um cliente
a.	Siga o exemplo criado para a classe Produto
 
DESAFIO
1.	Implemente seu projeto para possuir a classe Fornecedor com os atributos {id, cnpj, nome, endereco, email, telefone), com todas as operações de banco de dados explicadas neste tutorial.

2.	Pesquise como se faz e altere o seu projeto de forma que os Ids do Produto e do Cliente sejam chaves primárias
 
ETAPA5: IMPLEMENTANDO A CAMADA SERVICE
Classes service são invocadas pelo endpoints e responsáveis por executar a lógica de negócios a ser processada.
1.	Implemente classe ProductService com os métodos para manutenção de dados (CRUD – Create, Retrieve, Update & Delete):
a.	Crie uma pasta chamada services dentro do seu projeto
b.	Crie um arquivo chamado ProductService.cs dentro da pasta services
Implemente a classe ProductService:
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using loja.data;
using loja.models;

namespace loja.services
{
    public class ProductService
    {
        private readonly LojaDbContext _dbContext;

        public ProductService(LojaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // Método para consultar todos os produtos
        public async Task<List<Produto>> GetAllProductsAsync()
        {
            return await _dbContext.Produtos.ToListAsync();
        }

        // Métodd para consultar um produto a partir do seu Id
        public async Task<Produto> GetProductByIdAsync(int id)
        {
            return await _dbContext.Produtos.FindAsync(id);
        }

        // Método para  gravar um novo produto
        public async Task AddProductAsync(Produto produto)
        {
            _dbContext.Produtos.Add(produto);
            await _dbContext.SaveChangesAsync();
        }

        // Método para atualizar os dados de um produto
        public async Task UpdateProductAsync(Produto produto)
        {
            _dbContext.Entry(produto).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        // Método para excluir um produto
        public async Task DeleteProductAsync(int id)
        {
            var produto = await _dbContext.Produtos.FindAsync(id);
            if (produto != null)
            {
                _dbContext.Produtos.Remove(produto);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}


2.	Altere o Program.cs, adequando-o ao uso da classe sevice. Atenção: Os endpoints serão refatorados (reescritos) para uso da classe ProductService:
a.	Importante, compare as diferenças (utilize o GPT) para compreender no que o novo Programa.cs foi alterado;
b.	O novo Program.cs deve ficar assim:
using Microsoft.AspNetCore.Mvc;
using loja.models;
using loja.services;
using System.Collections.Generic;
using System.Threading.Tasks;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<ProductService>();

var app = builder.Build();

// Configurar as requisições HTTP 
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

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

3.	Teste os endpoints refatorados com o Postman.
a.	Verifique os dados no banco de dados, para garantir que tudo está funcionando
 
ETAPA6: PRATICANDO DA IMPLEMENTAÇÃO DE CLASSES SERVICE
1.	Implemente a classe FornecedorModel e FornecedorService e os respectivos endpoints no seu projeto
2.	Teste o endpoints da classe Fornecedor com o Postman

ETAPA7: HABILITANDO AUTENTICAÇÃO NOS ENDPOINTS DO SISTEMA
1.	Crie a funcionalidade para gravar dados de usuários. Deve-se gravar o nome, email (que será usado no emai) e senha. Implemente classes model e service, bem como os endpoints para criar e para consultar dados dos usuários.

2.	Utilize o código abaixo, que foi construído em sala de aula para implementar o login e a autenticação:
dotnet add package Microsoft.AspNetCore.Authentication.JwtBearer*/
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("abc"))
        };
    });

var app = builder.Build();

app.UseHttpsRedirection();

app.MapPost("/login", async (HttpContext context) =>
{
    //receber o request
    using var reader = new StreamReader(context.Request.Body);
    var body = await reader.ReadToEndAsync();

    //Deserializar o objeto
    var json = JsonDocument.Parse(body);
    var username = json.RootElement.GetProperty("username").GetString();
    var email = json.RootElement.GetProperty("email").GetString();
    var senha = json.RootElement.GetProperty("senha").GetString();

    //Esta parte do código será complementada com a service na próxima aula
    var token = "";
    if (senha == "1029")
    {
        token = GenerateToken(email); //O método generateToken será reimplementado em uma classe especializada
    }
    // return token;
    await context.Response.WriteAsync(token);
});

//Rota segura: : toda rota tem corpo de código parecido
app.MapGet("/rotaSegura", async (HttpContext context) =>
{
    //Verificar se o token está presente
    if (!context.Request.Headers.ContainsKey("Authorization"))
    {
        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
        await context.Response.WriteAsync("Token não fornecido");
    }

    //Obter o token
    var token = context.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

    //Validar o token
    /*Esta lógica será convertida em um método dentro de uma classe a ser reaproveitada*/
    var tokenHandler = new JwtSecurityTokenHandler();
    var key = Encoding.ASCII.GetBytes("abcabcabcabcabcabcabcabcabcabcabc"); //Chave secreta (a mesma utilizada para gerar o token)
    var validationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false
    };
    SecurityToken validateToken;
    try
    {
        //Decodifica, verifica e valida o token
        tokenHandler.ValidateToken(token, validationParameters, out validateToken);
    }
    catch (Exception)
    {
        //Caso o token seja inválido
        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
        await context.Response.WriteAsync("Token inválido");
    }
    //Se o token é válido: dar andamento na lógica do endpoint
    await context.Response.WriteAsync("Autorizado");
});


3.	Implemente o endpoint de login para o seu sistema, retornando um token jwt
4.	Implemente a autenticação em todos os endpoints do seu projeto
ETAPA8: IMPLEMENTANDO A FUNCIONALIDADE DE VENDAS
Esta questão representa a atividade avaliativa prática valendo 1,0 ponto
Entrega em 27/06/2024 impreterivelmente
1.	Implemente a funcionalidade de vendas para o seu sistema
2.	Cada venda deve possuir uma data da venda, número da nota fiscal, cliente, um produto, quantidade vendida e preço (de venda) unitário 
3.	Implemente a classe model, a classe service e as rotas (no Program.cs)
4.	Métodos que devem existir:
a.	Gravar uma venda: Deve-se validar se o cliente e o produto existem antes de grava a venda
b.	Consultar vendas por produto (detalhada): deve receber o id do produto e retornar o nome do produto, a data da venda, o id da venda, o nome do cliente, a quantidade vendida e o preço de venda.
Atenção: Será retornado um objeto para cada produto x venda

c.	Consultar vendas por produto (sumarizada): deve receber o id do produto e retornar o nome do produto, a soma das quantidades vendas e a soma dos preços cobrados.
Atenção: serão retornados  objetos com o total vendido (quantidade e preço) de cada produto
d.	Consultar vendas por cliente (detalhada): deve receber o id do cliente e retornar o nome do produto, a data da venda, o id da venda, a quantidade vendida e o preço de venda.
Atenção: Será retornado um objeto para cada produto x venda

e.	Consultar vendas por cliente (sumarizada): deve receber o id do cliente e retornar a soma dos preços cobrados, os produtos e respectivas quantidades.
Atenção: serão retornados  objetos com o total vendido (quantidade e preço) de cada produto



