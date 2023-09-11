using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MyApp_Auth
{
    public class User
    {
        [Key]
        public int UserID { get; set; }

        public int RoleID { get; set; }

        [Display(Name = "شماره همراه")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(11)]
        public string Phone { get; set; }

        [Display(Name = "کلمه عبور")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(80)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "تایید ایمیل")]
        [MaxLength(36, ErrorMessage = "نباید بیشتر از {1} کاراکتر باشد")]
        public string ActiveLink { get; set; }

        [Display(Name = "فعالیت")]
        public bool IsActive { get; set; }

        [ForeignKey("RoleID")]
        public virtual Role Role { get; set; }
    }
}