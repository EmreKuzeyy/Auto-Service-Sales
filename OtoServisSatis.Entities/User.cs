using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtoServisSatis.Entities
{
    public class User : IEntity
    {
        public int Id { get; set; }

        [Display(Name = "Kullanıcı Rolü"), Required(ErrorMessage = "{0} Boş Bırakılamaz")]
        public int RoleId { get; set; }

        [Display(Name = "Kullanıcı Rolü")]
        public virtual Role? Role { get; set; }

        [StringLength(50)]
        [Display(Name = "Ad") ,Required(ErrorMessage = "{0} Boş Bırakılamaz")]
        public  string Name { get; set; }

        [StringLength(50)]
        [Display(Name = "Soyad") , Required(ErrorMessage = "{0} Boş Bırakılamaz")]
        public string Surname { get; set; }

        [StringLength(50)]
        [Display(Name = "Mail Adresi") , Required(ErrorMessage = "{0} Boş Bırakılamaz")]
        public string Email { get; set; }

        [StringLength(20)]
        [Display(Name = "Telefon Numarası") , Required(ErrorMessage = "{0} Boş Bırakılamaz")]
        public string Phone { get; set; }

        [StringLength(50)]
        [Display(Name = "Kullanıcı Adı")]
        public string? UserName { get; set; }

        [StringLength(50)]
        [Display(Name = "Şifre") , Required(ErrorMessage = "{0} Boş Bırakılamaz")]
        public string Password { get; set; }
        [Display(Name = "Aktif mi"), Required(ErrorMessage = "{0} Boş Bırakılamaz")]
        public bool IsActive { get; set; }
        [Display(Name = "Eklenme Tarihi"), ScaffoldColumn(false)]
        public DateTime? DateAdded { get; set; } = DateTime.Now;

        public Guid? UserGuid { get; set; } = Guid.NewGuid();

    }
}
