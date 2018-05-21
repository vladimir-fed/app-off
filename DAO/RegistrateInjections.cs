using DAO.Contexts;
using DAO.Repositories;
using DAO.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using AutoMapper;
using Grace.DependencyInjection;

namespace DAO
{
    public static class RegistrateInjections
    {
        public static void Registrate(this IInjectionScope services, IConfiguration configuration)
        {
            if (!Enum.TryParse(configuration["Database"], out Database database))
            {
                throw new ArgumentException("Database configuration is not setted or has wrong value. Please check in appsetings.json");
            }

            services.Configure(c => c.ExportInstance(new MapperConfiguration(x => x.AddProfiles("DAO"))).As<AutoMapper.IConfigurationProvider>().Lifestyle.Singleton());
            services.Configure(c => c.Export<Mapper>().As<IMapper>().Lifestyle.Singleton());

            services.Configure(c => c.ExportInstance(GetDatabase(new DbContextOptionsBuilder<BlogContext>(), database, configuration).Options).As<DbContextOptions<BlogContext>>().Lifestyle.Singleton());
            services.Configure(c => c.Export<BlogContext>().Lifestyle.SingletonPerRequest());

            services.Configure(c => c.Export<UsersRepository>().As<IUsersRepository>().Lifestyle.SingletonPerRequest());
            services.Configure(c => c.Export<PostsRepository>().As<IPostsRepository>().Lifestyle.SingletonPerRequest());
            services.Configure(c => c.Export<CommentsRepository>().As<ICommentsRepository>().Lifestyle.SingletonPerRequest());

            services.Configure(c => c.Export<TokenService>().As<ITokenService>().Lifestyle.Singleton());
            services.Configure(c => c.Export<HashPasswordService>().As<IHashPasswordService>().Lifestyle.Singleton());

            services.Configure(c => c.Export<UsersService>().As<IUsersService>().Lifestyle.SingletonPerRequest());
            services.Configure(c => c.Export<PostsService>().As<IPostsService>().Lifestyle.SingletonPerRequest());
            services.Configure(c => c.Export<CommentsService>().As<ICommentsService>().Lifestyle.SingletonPerRequest());
        }

        private static DbContextOptionsBuilder GetDatabase(DbContextOptionsBuilder options, Database database, IConfiguration configuration)
        {
            switch (database)
            {
                case Database.MSQL:
                    {
                        options.UseSqlServer(configuration.GetConnectionString("BloggingDatabase"));
                        break;
                    }
                case Database.Postgres:
                    {
                        options.UseNpgsql(Parse(Environment.GetEnvironmentVariable("DATABASE_URL")));
                        break;
                    }
                default: throw new ArgumentException(nameof(database));
            }

            return options;
        }

        public static string Parse(string stringUrl)
        {
            if (Uri.TryCreate(stringUrl, UriKind.Absolute, out Uri url))
            {
                return $"host={url.Host};Port={url.Port};username={url.UserInfo.Split(':')[0]};password={url.UserInfo.Split(':')[1]};database={url.LocalPath.Substring(1)};pooling=true;";
            }
            throw new ArgumentException(nameof(stringUrl));
        }
    }
}
