using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyApp_Auth
{
    public class RegisterViewModel
    {
        [Display(Name = "شماره همراه")]
        [Required(ErrorMessage = "{0} را وارد کنید")]
        [MaxLength(11)]
        public string Phone { get; set; }

        [Display(Name = "کلمه عبور")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(16, ErrorMessage = "نباید بیشتر از {1} کلمه باشد")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "تکرار کلمه عبور")]
        [Required(ErrorMessage = "{0} را وارد کنید")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "رمز ها یکسان نیستند")]
        public string RepeatPassword { get; set; }
    }

    public class CompeleteRegisterViewModel
    {
        [MaxLength(11)]
        public string Phone { get; set; }

        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(16, ErrorMessage = "نباید بیشتر از {1} کلمه باشد")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "کد احراز هویت")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(6, ErrorMessage = "نباید بیشتر از {1} کلمه باشد")]
        public string CodeNumber { get; set; }
    }

    public class LoginViewModel
    {
        [Display(Name = "شماره همراه")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(11)]
        public string Phone { get; set; }

        [Display(Name = "کلمه عبور")]
        [Required(ErrorMessage = "{0} را وارد کنید")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}