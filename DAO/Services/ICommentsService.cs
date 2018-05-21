using System.Collections.Generic;
using DAO.Models;

namespace DAO.Services
{
    public interface ICommentsService
    {
        IEnumerable<Comment> GetAll();
        IEnumerable<Comment> GetAllByPostId(int id);
        Comment GetById(int id);
        void Update(Comment comment);
        void Create(Comment comment);
        void Delete(int id);
    }
}
