using System;
using System.Linq;
using DAO.Contexts;
using DAO.Models;
using Microsoft.EntityFrameworkCore;

namespace DAO.Repositories
{
    public class CommentsRepository : Repository<Comment>, ICommentsRepository
    {
        private readonly BlogContext _blogContext;

        public override DbContext Context => _blogContext;

        public override DbSet<Comment> Entities => _blogContext.Comments;

        public CommentsRepository(BlogContext blogContext)
        {
            _blogContext = blogContext ?? throw new ArgumentNullException(nameof(blogContext));
        }

        public IQueryable<Comment> GetAllByPostId(int postId)
        {
            return _blogContext.Comments.Where(x => x.PostId == postId).Include(c => c.User);
        }

        public IQueryable<Comment> GetAllByPost(Post post)
        {
            return GetAllByPostId(post.Id);
        }
    }
}
