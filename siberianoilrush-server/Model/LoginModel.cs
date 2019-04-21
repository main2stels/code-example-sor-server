using System.ComponentModel.DataAnnotations;

namespace siberianoilrush_server.Model
{
    public class LoginModel
    {
        [Required]
        public string Login { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
