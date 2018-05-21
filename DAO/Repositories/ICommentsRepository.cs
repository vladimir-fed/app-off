using System.Linq;
using DAO.Models;

namespace DAO.Repositories
{
    public interface ICommentsRepository : IRepository<Comment>
    {
        IQueryable<Comment> GetAllByPostId(int postId);
        IQueryable<Comment> GetAllByPost(Post post);
    }
}
