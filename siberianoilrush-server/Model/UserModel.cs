using System.ComponentModel.DataAnnotations;

namespace siberianoilrush_server.Model
{
    public class UserModel
    {
        [Required]
        public string Nickname { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string Email { get; set; }
    }
}
