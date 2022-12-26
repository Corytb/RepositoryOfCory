using System.ComponentModel.DataAnnotations.Schema;

namespace IAmTraveling.Models.Tables
{
    public class Thing
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int LocationId { get; set; }
        public DateTime ThingDate { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime AddedDate { get; set; }
        public string Description { get; set; }
    }
}
