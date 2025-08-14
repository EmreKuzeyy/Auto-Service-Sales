using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtoServisSatis.Entities
{
    public class Customer : IEntity
    {
        public int Id { get; set; }

        [Display(Name = "Araç No"), Required(ErrorMessage = "{0} Boş Bırakılamaz")]
        public int VehicleId { get; set; }

        [StringLength(50)]
        [Display(Name = "Adı"), Required(ErrorMessage = "{0} Boş Bırakılamaz")]
        public string Name { get; set; }

        [StringLength(50)]
        [Display(Name = "Soyadı"), Required(ErrorMessage = "{0} Boş Bırakılamaz")]
        public string Surname { get; set; }

        [StringLength(11)]
        [Display(Name = "Kimlik No")]
        public string? IdentityNumber { get; set; }

        [StringLength(50)]
        [Display(Name = "Mail Adresi")]
        public string? Email { get; set; }

        [StringLength(500)]
        [Display(Name = "Adres")]
        public string? Address { get; set; }

        [StringLength(15)]
        [Display(Name = "Telefon Numarası"), Required(ErrorMessage = "{0} Boş Bırakılamaz")]
        public string Phone { get; set; }

        [Display(Name = "Not")]
        public string? Notes { get; set; }

        [Display(Name = "Araç")]
        public virtual Vehicle? Vehicle { get; set; }
    }
}
