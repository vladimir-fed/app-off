using DAO.DTO;

namespace DAO.Services
{
    public interface ITokenService
    {
        TokenDto GenerateToken(UserDto user);
    }
}
