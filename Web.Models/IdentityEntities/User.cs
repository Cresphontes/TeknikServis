using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Web.Models.Entities;

namespace Web.Models.IdentityEntities
{
    public class User:IdentityUser
    {
     
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Gender { get; set; }
        public DateTime BirthDate { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Adress { get; set; }
        public string ActivationCode { get; set; }

        public virtual ICollection<TroubleRecord> TroubleRecords { get; set; }

    }
}
