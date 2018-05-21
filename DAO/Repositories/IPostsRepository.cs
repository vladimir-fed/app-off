using DAO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAO.Repositories
{
    public interface IPostsRepository : IRepository<Post>
    {
        IQueryable<Post> GetAllByUserId(int id);

        IQueryable<Post> GetAllByUser(User user);
    }
}
