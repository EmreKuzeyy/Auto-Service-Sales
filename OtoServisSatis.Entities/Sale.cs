using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtoServisSatis.Entities
{
    public class Sale : IEntity
    {
        public int Id { get; set; }

        [Display(Name = "Araç"), Required(ErrorMessage = "{0} Boş Bırakılamaz")]
        public int VehicleId { get; set; }

        [Display(Name = "Müşteri"), Required(ErrorMessage = "{0} Boş Bırakılamaz")]
        public int CustomerId { get; set; }

        [Display(Name = "Satış Fiyatı"), Required(ErrorMessage = "{0} Boş Bırakılamaz")]
        public decimal SalePrice { get; set; }

        [Display(Name = "Satış Tarihi"), Required(ErrorMessage = "{0} Boş Bırakılamaz")]
        public DateTime SaleDate { get; set; }

        [Display(Name = "Araç")]
        public virtual Vehicle? Vehicle { get; set; }

        [Display(Name = "Müşteri")]
        public virtual Customer? Customer { get; set; }
    }
}
