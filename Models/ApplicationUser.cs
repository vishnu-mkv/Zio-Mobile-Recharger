using Microsoft.AspNetCore.Identity;

namespace mobile_recharger.Models
{
    public class ApplicationUser:IdentityUser
    {

        public virtual List<Recharge> Recharges { get; set; } = new();
    }
}
