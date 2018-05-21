using System.Collections.Generic;
using DAO.DTO;
using DAO.Models;

namespace DAO.Services
{
    public interface IUsersService
    {
        IEnumerable<User> GetAll();
        User GetById(int id);
        void Update(User user);
        TokenDto Create(SignUpDto user);
        void Delete(int id);

        TokenDto Login(LoginDTO loginDto);
    }
}
