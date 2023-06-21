using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WorkFlowX.Models.Enums;

namespace WorkFlowX.Models
{
    public class Team
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public TeamStatusEnum Status { get; set; }

        #region Navigations
        //public User User { get; set; }
        #endregion

        #region Collections
        public List<Project> Projects { get; set; }
        public List<User> Users { get; set; }
        #endregion
    }

}