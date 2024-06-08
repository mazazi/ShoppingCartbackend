using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Tatweer.Insrastructure.Data;

namespace Luftborn.Infrastructure.Data;

public class TatweerContextFactory : IDesignTimeDbContextFactory<TatweerContext>
{
    public TatweerContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<TatweerContext>();
        optionsBuilder.UseSqlServer("Data Source=.\\SQLEXPRESS;Initial Catalog=ShoppingCartDB;User ID=azazi;Password=MAma@12345;Encrypt=True;TrustServerCertificate=True;");
        return new TatweerContext(optionsBuilder.Options);
    }
}