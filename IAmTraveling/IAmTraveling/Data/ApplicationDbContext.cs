using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace IAmTraveling.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Thing> Things { get; set; }
        public DbSet<ThingLocation> ThingLocations { get; set; }
        public DbSet<MediaFile> MediaFiles { get; set; }
        public DbSet<Comment> Comments { get; set; }


        public class Thing
        {
            public int Id { get; set; }
            public string UserId { get; set; }
            public DateTime ThingDate { get; set; }

            [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            public DateTime AddedToDbDate { get; set; }

            [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
            public DateTime UpdateDate { get; set; }
            public string Description { get; set; }
        }
        public class ThingLocation
        {
            public string Id { get; set; }
            public string PlaceName { get; set; }
            public string TypeOfPlace { get; set; }
            public string ProvinceOrState { get; set; }
            public string Country { get; set; }
            public string FormattedAddress { get; set; }

            //[Column(TypeName = "double(18,15)")]
            public double LatitudeCoord { get; set; }
            public double LongitudeCoord { get; set; } //(decimal (18,15) enough, I believe
        }

        public class MediaFile
        {
            public int Id { get; set; }
            public int ThingId { get; set; }
            public string FileName { get; set; }
        }

        public class Comment
        {
            public int Id { get; set; }
            public int ThingId { get; set; }
            public string Text { get; set; }
        }
    }
}