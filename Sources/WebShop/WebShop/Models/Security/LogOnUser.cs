using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
namespace WebShop.Models.Security
{
    public class LogOnUser
    {
        [Required]
        [DisplayName("User name")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [DisplayName("Password")]
        public string Password { get; set; }

        [DisplayName("Remember me?")]
        public bool RememberMe { get; set; }
    }
}