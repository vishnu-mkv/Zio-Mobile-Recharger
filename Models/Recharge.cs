using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace mobile_recharger.Models
{
    public class Recharge
    {

        [Key]
        public int RechargeId { get; set; }
        public string UserId { get; set; }

        public virtual ApplicationUser? User { get; set; }

        public int RechargePlanId { get; set; }

        public virtual RechargePlan? RechargePlan { get; set; }

        public DateTime RechargedOn { get; set; } = new DateTime();

        public virtual DateTime ValidTill { get; set; }

        public string mobileNumber { get; set; }

        public void SetValidTill()
        {
            ValidTill = DateTime.Today.AddDays(RechargePlan?.Validity ?? 0);
        }
    }
}
