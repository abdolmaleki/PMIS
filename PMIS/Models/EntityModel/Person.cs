using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PMIS.Models.EntityModel
{
    public class Person

    {

        public int ID { get; set; }
        [Display(Name = "نام")]
        [Required(ErrorMessage = "نام را وارد نکرده اید", AllowEmptyStrings = false)]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "نام خانوادگی را وارد نکرده اید", AllowEmptyStrings = false)]
        [Display(Name = "نام خانوادگی")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "کد ملی را وارد نکرده اید", AllowEmptyStrings = false)]
        [Display(Name = "کد ملی")]
        public int NationalCode { get; set; }
        [Display(Name = "نام پدر")]
        public string FatherName { get; set; }
        [Display(Name = "تاریخ تولد")]
        public string BirthDay { get; set; }
        [Display(Name = "نوع عضویت")]
        public int RegisterType { get; set; }
        [Display(Name = "جایگاه سازمانی")]
        public int OrganizationStatus { get; set; }
        [Display(Name = "سمت")]
        public int OrganizationPosition { get; set; }
        [Display(Name = "موبایل")]
        public string Mobile { get; set; }
        [Display(Name = "تلفن")]
        public string Tell { get; set; }
        [Display(Name = "ایمیل")]
        public string Email { get; set; }
        [Display(Name = "آدرس")]
        public string Address { get; set; }
        public virtual User User { get; set; }
        [ForeignKey("Organization")]
        public int OrganizationId { get; set; }
        public virtual Organization Organization { get; set; }

    }
}