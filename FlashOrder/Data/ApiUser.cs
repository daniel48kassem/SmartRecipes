

using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace FlashOrder.Data
{
    public class ApiUser:IdentityUser
    {
        public string DisplayName { get; set; }
        public string Bio { get; set; }
        
        public virtual IList<Follow> ChefFollowers { get; set; }
        public virtual IList<Follow> FollowedChefs { get; set; }
        
        
        public virtual IList<Rating> MyRatedRecipes { get; set; }
        
        // public virtual HashSet<ApiUser> ChefFollowers { get; set; }
        // public virtual HashSet<ApiUser> MyFollowedChefs { get; set; }
    }
}