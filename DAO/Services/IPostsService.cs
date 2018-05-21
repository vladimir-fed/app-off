using System.Collections.Generic;
using DAO.DTO;

namespace DAO.Services
{
    public interface IPostsService
    {
        IEnumerable<PostDto> GetAll();
        IEnumerable<PostDto> GetAllByUserId(int userId);
        PostDto GetById(int id);
        void Update(PostDto post);
        void Create(PostDto post);
        void Delete(int id);
    }
}
