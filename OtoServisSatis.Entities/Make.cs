using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtoServisSatis.Entities
{
    public class Make : IEntity
    {
        public int Id { get; set; }

        [StringLength(20)]
        [Display(Name = "Marka Adı"), Required(ErrorMessage = "{0} Boş Bırakılamaz")]
        public string Name { get; set; }
    }
}
