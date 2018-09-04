using PMIS.Models.EntityModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web;

namespace PMIS.Models
{
    public class User
    {
        [Key, ForeignKey("Person")]
        public int ID { get; set; }
        [Display(Name = "نام کاربری")]
        [StringLength(200)]
        [Required(ErrorMessage = "نام کاربری را وارد نکرده اید", AllowEmptyStrings = false)]
        public string UserName { get; set; }
        [Display(Name = "رمز عبور")]
        [Required(ErrorMessage = "رمز عبور را وارد نکرده اید", AllowEmptyStrings = false)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Display(Name = "نام کاربر")]
        public string FullName { get; set; }
        [Display(Name = "عکس کاربری")]
        public string ImagePath { get; set; }
        [Display(Name = "سطح دسترسی")]
        public int UserRole { get; set; }
        public virtual Person Person { get; set; }
        [NotMapped]
        public HttpPostedFileBase ImageFile { get; set; }


    }
}