using System.Linq;
using System.Security.Cryptography;
using System.Text;
using DAO.Models;

namespace DAO.Services
{
    public class HashPasswordService : IHashPasswordService
    {
        private readonly RNGCryptoServiceProvider _rngCrypto = new RNGCryptoServiceProvider();
        private readonly SHA512 _sha512 = SHA512.Create();

        public void AddSaltAndHashPassword(User user)
        {
            var salt = new byte[512 / 8];
            _rngCrypto.GetNonZeroBytes(salt);

            user.Salt = salt;
            user.Password = _sha512.ComputeHash(Encoding.UTF8.GetBytes(user.PasswordText).Concat(salt).ToArray());
        }

        public bool PasswordEqualsUserPassword(string password, User user)
        {
            var hash = _sha512.ComputeHash(Encoding.UTF8.GetBytes(password).Concat(user.Salt).ToArray());

            return hash.SequenceEqual(user.Password);
        }
    }
}
