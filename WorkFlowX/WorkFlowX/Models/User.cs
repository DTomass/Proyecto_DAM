using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WorkFlowX.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string UserName { get; set; }
        public string UserMail { get; set; }
        public string UserPassword { get; set; }
        [NotMapped]
        public string ConfirmPassword { get; set; }
        public bool NeedRestore { get; set;}
        public bool HasConfirmation { get; set; }
        public string Token { get; set; }
        public int CompanyId { get; set; }
        public int RoleId { get; set; }
        public string UserSurname { get; set; }
        public string UserAddress { get; set; }
        public string UserPhone { get; set; }
        public string Gender { get; set; }
        public DateTime? Birthdate { get; set; }
        public int? TeamId { get; set; }


        #region Navigations
        public Role Role { get; set; }
        public Company Company { get; set; }
        public Team Team { get; set; }
        #endregion
    }
}