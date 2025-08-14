using System.ComponentModel.DataAnnotations;

namespace OtoServisSatis.WebUI.Models
{
    public class CustomerLoginViewModel
    {
        [StringLength(50)]
        [Display(Name = "Email"), Required(ErrorMessage = "{0} Boş Bırakılamaz")]
        public string Email { get; set; }

        [StringLength(50)]
        [Display(Name = "Şifre"), Required(ErrorMessage = "{0} Boş Bırakılamaz")]
        public string Password { get; set; }
    }
}
