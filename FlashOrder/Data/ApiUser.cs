

using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace FlashOrder.Data
{
    public class ApiUser:IdentityUser
    {
        public string DisplayName { get; set; }
        public string Bio { get; set; }
        
        // public ICollection<ApiUser> Followers { get; set; }
        // public ICollection<ApiUser> Followeds { get; set; }
        
        public virtual IList<Follow> ChefFollowers { get; set; }
        public virtual IList<Follow> FollowedChefs { get; set; }
        
        // public virtual ICollection<Relation> RelatedTo { get; set; }
        // public virtual ICollection<Relation> RelatedFrom { get; set; }
    
        // public virtual HashSet<ApiUser> ChefFollowers { get; set; }
        // public virtual HashSet<ApiUser> MyFollowedChefs { get; set; }
    }
}