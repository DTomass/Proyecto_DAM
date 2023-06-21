using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WorkFlowX.Models.Enums;

namespace WorkFlowX.Models
{
    public class Task
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime? ExpectedEndDate { get; set; }
        public TaskStatusEnum? Status { get; set; }
        public int? Priority { get; set; }
        public string Comment { get; set; }
        public int UserId { get; set; }

        #region Navigations
        public Project Project { get; set; }
        public User User { get; set; }
        #endregion

        #region Collections
        #endregion
    }

}