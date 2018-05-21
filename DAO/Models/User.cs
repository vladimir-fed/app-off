using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAO.Models
{
    public class User
    {
        public int Id { get; set; }

        public string UserName { get; set; }

        [NotMapped]
        public string PasswordText { get; set; }

        public byte[] Password { get; set; }

        public byte[] Salt { get; set; }

        public string Name { get; set; }

        public List<Post> Posts { get; set; }
    }
}
