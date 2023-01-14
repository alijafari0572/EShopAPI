using System.ComponentModel.DataAnnotations;

namespace EShopAPI.Models
{
    public class Login_model
    {
        [Required]
        public string  UserName { get; set; }
        [Required]
        public string PassWord { get; set; }
    }
}
