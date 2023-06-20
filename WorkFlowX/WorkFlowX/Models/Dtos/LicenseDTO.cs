using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WorkFlowX.Models.Dtos
{
    public class LicenseDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string AreaName { get; set; }
        public string NameWithArea
        {
            get
            {
                return $"{Name}/{AreaName}";
            }
        }
    }
}