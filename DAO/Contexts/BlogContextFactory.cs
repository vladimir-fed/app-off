using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace DAO.Contexts
{
    public class BloggingContextFactory : IDesignTimeDbContextFactory<BlogContext>
    {
        public BlogContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<BlogContext>();
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=Blog.MyDB;Trusted_Connection=True;ConnectRetryCount=0");

            return new BlogContext(optionsBuilder.Options);
        }
    }
}