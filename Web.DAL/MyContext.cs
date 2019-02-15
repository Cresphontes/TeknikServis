using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Models.Entities;

namespace Web.DAL
{
    public class MyContext:DbContext
    {
        public MyContext():base("name=MyCon")
        {

        }

        public virtual DbSet<ArizaKayit> ArizaKayits { get; set; }
    }
}
