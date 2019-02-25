using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Models.Entities;
using Web.Models.EntityIdentity;
using Web.Models.IdentityEntities;

namespace Web.DAL
{
    public class MyContext:IdentityDbContext<User>
    {
        public MyContext():base("name=MyCon")
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public virtual DbSet<TroubleRecord> TroubleRecords { get; set; }
        //public virtual DbSet<UserTroubleRecord> UserTroubleRecords { get; set; }
    }
}
