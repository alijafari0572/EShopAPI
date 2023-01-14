using System.ComponentModel.DataAnnotations;

namespace WebClient.Models
{
    public class Login_Viewmodel
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
