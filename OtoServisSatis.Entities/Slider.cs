using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtoServisSatis.Entities
{
    public class Slider: IEntity
    {
        public int Id { get; set; }

        [StringLength(150), Display(Name = "Başlık")]
        public string? Title { get; set; }

        [StringLength(500), Display(Name = "Açıklama")]
        public string? Description { get; set; }

        [StringLength(200), Display(Name = "Resim")]
        public string? Photo { get; set; }

        [StringLength(200)]
        public string? Link { get; set; }
    }
}
