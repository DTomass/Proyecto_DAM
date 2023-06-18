using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WorkFlowX.Models
{
    public class Company
    {
        [Key]
        public int Id { get; set; }
        public string CompanyName { get; set; }
        public string CompanyNif { get; set; }
        public string CompanyAddress { get; set; }
        public string CompanyPhone { get; set; }
        public string CompanyMail { get; set; }
        public string CompanyWeb { get; set; }
        public string CompanyCountry { get; set; }
        public string CompanyCity { get; set; }
        public DateTime? RegisterDate { get; set; }
    }
}