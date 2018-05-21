using System;

namespace DAO.DTO
{
    public class TokenDto
    {
        public string Token { get; set; }

        public DateTimeOffset ExpirationDate { get; set; }
    }
}
