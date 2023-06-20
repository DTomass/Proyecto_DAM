using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WorkFlowX.Models.Dtos
{
    public class RoleDTO
    {
        public int Id { get; set; }
        public string RoleName { get; set; }

        public List<int> LicenseIds { get; set; }
        public List<LicenseDTO> Licenses { get; set; }
    }
}