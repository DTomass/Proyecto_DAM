using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WorkFlowX.Models
{
    public class Project
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime? ExpectedEndDate { get; set; }
        public int? Budget { get; set; }
        public int? ActualCost { get; set; }
        [NotMapped]
        public List<int> TeamIds { get; set; }

        #region Navigations
        public Company Company { get; set; }
        #endregion

        #region Collections
        public List<Team> Teams { get; set; }
        public List<Task> Tasks { get; set; }
        #endregion
    }

}