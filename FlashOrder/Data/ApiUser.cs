using Microsoft.AspNetCore.Identity;

namespace FlashOrder.Data
{
    public class ApiUser:IdentityUser
    {
        public string DisplayName { get; set; }
        public string Bio { get; set; }
    }
}