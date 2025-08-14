using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtoServisSatis.Entities
{
    public class Service : IEntity
    {
        public int Id { get; set; }

        [Display(Name = "Servise Geliş Tarihi")]
        public DateTime ServiceEntryDate { get; set; }

        [Display(Name = "Servisten Çıkış Tarihi")]
        public DateTime ServiceExitDate { get; set; }

        [Display(Name = "Araç Durumu")]
        public string VehicleIssue { get; set; }

        [Display(Name = "Servis Ücreti")]
        public decimal ServiceCost { get; set; }

        [Display(Name = "Yapılan İşlemler") , Required(ErrorMessage = "{0} Boş Bırakılamaz")]
        public string PerformedOperations { get; set; }

        [Display(Name = "Garantisi Devam Ediyor Mu") , Required(ErrorMessage = "{0} Boş Bırakılamaz")]
        public bool IsUnderWarranty { get; set; }

        [StringLength(15)]
        [Display(Name = "Araç Plakası"), Required(ErrorMessage = "{0} Boş Bırakılamaz")]
        public string LicensePlate { get; set; }

        [StringLength(50)]
        [Display(Name = "Araç Markası"), Required(ErrorMessage = "{0} Boş Bırakılamaz")]
        public string Make { get; set; }

        [StringLength(50)]
        [Display(Name = "Araç Modeli"), Required(ErrorMessage = "{0} Boş Bırakılamaz")]
        public string? Model { get; set; }

        [StringLength(50)]
        [Display(Name = "Araç Kasa Tipi")]
        public string? BodyType { get; set; }

        [StringLength(50)]
        [Display(Name = "Araç Şase Numarası"), Required(ErrorMessage = "{0} Boş Bırakılamaz")]
        public string ChassisNumber { get; set; }

        [Display(Name = "Not")]
        public string? Notes { get; set; }


    }
}
