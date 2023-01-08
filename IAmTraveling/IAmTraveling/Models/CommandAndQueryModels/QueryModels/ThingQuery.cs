using Microsoft.AspNetCore.Identity;

namespace IAmTraveling.Models.CommandAndQueryModels.QueryModels
{
    public class ThingQuery
    {
        public IdentityUser Id { get; set; }
        public int ThingId { get; set; }
        public DateTime? ThingDate { get; set; }

    }
}
