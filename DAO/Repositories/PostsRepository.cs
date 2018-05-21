using DAO.Contexts;
using DAO.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace DAO.Repositories
{
    public class PostsRepository : Repository<Post>, IPostsRepository
    {
        private readonly BlogContext _blogContext;

        public override DbContext Context => _blogContext;

        public override DbSet<Post> Entities => _blogContext.Posts;

        public PostsRepository(BlogContext blogContext)
        {
            _blogContext = blogContext ?? throw new ArgumentNullException(nameof(blogContext));
        }

        public override IQueryable<Post> GetAll()
        {
            return base.GetAll().Include(p => p.User);
        }

        public IQueryable<Post> GetAllByUserId(int id)
        {
            return _blogContext.Posts.Where(p => p.UserId == id);
        }

        public IQueryable<Post> GetAllByUser(User user)
        {
            return GetAllByUserId(user.Id);
        }
    }
}
