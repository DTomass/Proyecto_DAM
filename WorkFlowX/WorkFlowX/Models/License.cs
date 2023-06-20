using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WorkFlowX.Models
{
    public class License
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int AreaId { get; set; }

        #region Navigations
        public Area Area { get; set; }
        #endregion

        #region Collections
        [JsonIgnore]
        public List<Role> Roles { get; set; }
        #endregion
    }
}