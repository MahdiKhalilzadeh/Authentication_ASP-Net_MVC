using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyApp_Auth
{
    public class Role
    {
        [Key]
        public int RoleID { get; set; }

        [Display(Name = "نام نقش")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(20)]
        public string Name { get; set; }

        [Display(Name = "سربرگ")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(30)]
        public string Title { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}