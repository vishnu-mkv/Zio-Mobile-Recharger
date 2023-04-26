using System.ComponentModel.DataAnnotations;

namespace mobile_recharger.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }
        public string Name { get; set; } = "";
        public bool data { get; set; }
        public bool call { get; set; }
        public bool sms { get; set; }

        public virtual List<RechargePlan> RechargePlans { get; set; } = new();
    }
}
