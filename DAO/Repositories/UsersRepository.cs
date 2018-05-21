using System;
using DAO.Contexts;
using DAO.Models;
using Microsoft.EntityFrameworkCore;

namespace DAO.Repositories
{
    public class UsersRepository : Repository<User>, IUsersRepository
    {
        private readonly BlogContext _blogContext;

        public override DbContext Context => _blogContext;

        public override DbSet<User> Entities => _blogContext.Users;

        public UsersRepository(BlogContext blogContext)
        {
            _blogContext = blogContext ?? throw new ArgumentNullException(nameof(blogContext));
        }
    }
}
