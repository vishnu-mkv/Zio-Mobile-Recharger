using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace mobile_recharger.Models
{
    public class RechargePlan
    {
        [Key]
        public int RechargePlanId { get; set; }
        public int Price { get; set; }
        public int Validity { get; set; }

        public int CategoryId { get; set; }

        public virtual Category? Category { get; set; }
    }

}
