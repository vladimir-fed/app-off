using DAO.Models;

namespace DAO.Services
{
    public interface IHashPasswordService
    {
        void AddSaltAndHashPassword(User user);

        bool PasswordEqualsUserPassword(string password, User user);
    }
}
