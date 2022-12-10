using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Northwind.Services.EntityFrameworkCore.Blogging.Context
{
    /// <summary>
    /// DesignTimeBloggingContextFactory class.
    /// </summary>
    /// <seealso cref="Microsoft.EntityFrameworkCore.Design.IDesignTimeDbContextFactory&lt;Northwind.Services.EntityFrameworkCore.Blogging.Context.BloggingContext&gt;" />
    public class DesignTimeBloggingContextFactory : IDesignTimeDbContextFactory<BloggingContext>
    {
        /// <summary>
        /// Creates a new instance of a derived context.
        /// </summary>
        /// <param name="args">Arguments provided by the design-time service.</param>
        /// <returns>
        /// An instance of <typeparamref name="TContext" />.
        /// </returns>
        public BloggingContext CreateDbContext(string[] args)
        {
            const string connectionStringName = "SQLCONNSTR_NORTHWIND_BLOGGING";

            var configuration = new ConfigurationBuilder().AddEnvironmentVariables().Build();
            var connectionString = configuration.GetConnectionString(connectionStringName);

            if (string.IsNullOrEmpty(connectionString))
            {
                throw new Exception($"{connectionStringName} environment variable is not set.");
            }

            Console.WriteLine($"Using {connectionStringName} environment variable as a connection string.");

            var builderOptions = new DbContextOptionsBuilder<BloggingContext>().UseSqlServer(connectionString).Options;
            return new BloggingContext(builderOptions);
        }
    }
}
