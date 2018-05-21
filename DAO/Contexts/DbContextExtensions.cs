using DAO.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using DAO.Services;

namespace DAO.Contexts
{
    public static class DbContextExtensions
    {
        public static void EnsureSeeded(this BlogContext context, IHashPasswordService service)
        {
            if (!context.Users.Any())
            {
                var users = JsonConvert.DeserializeObject<List<User>>(File.ReadAllText($"seed{Path.DirectorySeparatorChar}users.json"));

                users.ForEach(service.AddSaltAndHashPassword);

                context.AddRange(users);
                context.SaveChanges();
            }
        }
    }
}
