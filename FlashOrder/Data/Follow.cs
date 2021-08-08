using System.ComponentModel.DataAnnotations;

namespace FlashOrder.Data
{
    public class Follow
    {
        [Required]
        public string ChefId { get; set; }
        public ApiUser Chef { get; set; }
        
        [Required]
        public string FollowerId { get; set; }
        public ApiUser Follower { get; set; }
    }
}