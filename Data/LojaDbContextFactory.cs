using loja.Data.loja.data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace loja.data{
public class LojaDbContextFactory : IDesignTimeDbContextFactory<LojaDbContext>{
        public LojaDbContext CreateDbContext (string [] args){

            var optonsBuilder = new DbContextOptionsBuilder<LojaDbContext>();

            IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsetings.json")
            .Build();

           var optionsBuilder = new DbContextOptionsBuilder<LojaDbContext>();
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            optionsBuilder.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 26)));

            return new LojaDbContext(optionsBuilder.Options);
        }
        
    }
}