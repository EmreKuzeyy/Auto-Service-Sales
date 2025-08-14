using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtoServisSatis.Entities
{
    public class Vehicle : IEntity
    {
        public int Id { get; set; }


        [Display(Name = "Markalar"), Required(ErrorMessage = "{0} Boş Bırakılamaz")]
        public int MakeId { get; set; }

        [StringLength(50)]
        [Display(Name = "Renk"), Required(ErrorMessage = "{0} Boş Bırakılamaz")]
        public string Colour { get; set; }

        [Display(Name = "Fiyat"), Required(ErrorMessage = "{0} Boş Bırakılamaz")]
        public decimal Price { get; set; }

        [StringLength(50)]
        [Display(Name = "Model"), Required(ErrorMessage = "{0} Boş Bırakılamaz")]
        public string Model { get; set; }

        [StringLength(50)]
        [Display(Name = "Kasa Tipi"), Required(ErrorMessage = "{0} Boş Bırakılamaz")]
        public string BodyType { get; set; }

        [Display(Name = "Model Tarihi"), Required(ErrorMessage = "{0} Boş Bırakılamaz")]
        public int ModelYear { get; set; }

        [Display(Name = "Satılık mı"), Required(ErrorMessage = "{0} Boş Bırakılamaz")]
        public bool IsSale { get; set; }

        [Display(Name = "Ana Sayfa?")]
        public bool HomePage { get; set; }

        [StringLength (200), Display(Name = "Fotoğraf 1")]
        public string? Photo1 { get; set; }

        [StringLength(200), Display(Name = "Fotoğraf 2")]
        public string? Photo2 { get; set; }

        [StringLength(200), Display(Name = "Fotoğraf 3")]
        public string? Photo3 { get; set; }

        [Display(Name = "Not")]
        public string? Notes { get; set; }

        [Display(Name = "Marka")]
        public virtual Make? Make { get; set; }
    }
}
