using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WorkFlowX.Models
{
    public class DtoUser
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string UserMail { get; set; }
        public string UserPassword { get; set; }
        public string ConfirmPassword { get; set; }
        public bool Restore { get; set;}
        public bool Confirmation { get; set; }
        public string Token { get; set; }
        public int ComId { get; set; }
    }
}