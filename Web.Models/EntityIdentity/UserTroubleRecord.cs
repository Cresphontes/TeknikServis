using System.ComponentModel.DataAnnotations.Schema;
using Web.Models.Entities;
using Web.Models.IdentityEntities;

namespace Web.Models.EntityIdentity
{
    public class UserTroubleRecord:BaseEntity2<string,int>
    {
    
        [ForeignKey("Id")]
        public virtual User User { get; set; }


        [ForeignKey("Id2")]
        public virtual TroubleRecord TroubleRecord { get; set; }
    }
}
