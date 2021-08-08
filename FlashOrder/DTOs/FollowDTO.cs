using System.ComponentModel.DataAnnotations;

namespace FlashOrder.DTOs
{
    public class FollowDTO
    {
        [Required]
        public string ChefId { get; set; }
    }
}