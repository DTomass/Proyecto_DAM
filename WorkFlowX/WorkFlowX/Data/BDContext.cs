using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using WorkFlowX.Models;
using System.Data;
using System.Data.SqlClient;
using System.Reflection.Emit;
using System.Data.Entity;

namespace WorkFlowX.Data
{
    public class Context : DbContext
    {
        public Context():base("WorkFlowXConnection")
        {
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Company> Companies { get; set; }
    }
}