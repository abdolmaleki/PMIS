using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PMIS.Models.EntityModel
{
    public class Organization
    {
        public int ID { get; set; }
        [StringLength(200)]
        [Required(ErrorMessage = "نام ساختار  را وارد نکرده اید", AllowEmptyStrings = false)]
        [Display(Name = "نام")]
        public string Name { get; set; }
        [Display(Name = "ماموریت")]
        public string Mission { get; set; }
        [Required(ErrorMessage = "وزن ساختار  را وارد نکرده اید", AllowEmptyStrings = false)]
        [Display(Name = "وزن ساختار")]
        public int Weight { get; set; }
        [Display(Name = "آدرس")]
        public string Address { get; set; }
        [Display(Name = "تلفن")]
        public string Tell { get; set; }

        public ICollection<Person> Persons { get; set; }

        public int? SuperOrganizationId { get; set; }


        public virtual Organization SuperOrganization { get; set; }

        public virtual ICollection<Organization> SubOrganizations { get; set; }


    }
}