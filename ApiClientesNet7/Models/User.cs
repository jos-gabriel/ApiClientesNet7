using System.ComponentModel.DataAnnotations;

namespace ApiClientesNet7.Models
{
    public class User
    {

        [Key]
        public int Id { get; set; }

        public string UserName { get; set; }

        public byte[] PasswordHash { get; set; }

        public byte[] PasswordSalt { get; set; }

    }
}
