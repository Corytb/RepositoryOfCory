using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System.ComponentModel.DataAnnotations;
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
        public DbSet<TravelCompanion> TravelCompanions { get; set; }


        public class Thing
        {
            [Key]
            public int ThingId { get; set; }
            [Comment("User-provided: date of the thing or event")]
            public DateTime? ThingDate { get; set; }

            [Comment("System-provided: DateTime added to database")]
            [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            public DateTime AddedToDbDate { get; set; }

            [Comment("System-provided: DateTime of most recent updates to the particular entry")]
            [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
            public DateTime UpdateDate { get; set; }

            [Comment("Description of the event, that user can add for their memories")]
            public string? Description { get; set; }

            public ThingLocation? ThingLocation { get; set; }
            public List<MediaFile>? MediaFiles { get; set; }
            public List<Comment>? Comments { get; set; }

            public IdentityUser UserId { get; set; }

        }

        public class ThingLocation
        {
            [Key]
            public int Id { get; set; }
            [Comment("PlaceId and subsequent fields are provided by GoogleMaps (or other online map API)")]
            public string? PlaceId { get; set; }
            public string? PlaceName { get; set; }
            public string? TypeOfPlace { get; set; }
            public string? ProvinceOrState { get; set; }
            public string? Country { get; set; }
            public string? FormattedAddress { get; set; }

            //[Column(TypeName = "double(18,15)")]
            public double? LatitudeCoord { get; set; }
            public double? LongitudeCoord { get; set; } //(decimal (18,15) enough, I believe

            public List<Thing> Things { get; set; }
        }

        public class MediaFile
        {
            [Key]
            public int MediaFileId { get; set; }
            public string FileName { get; set; }

            public Thing Thing { get; set; }
        }

        [Comment("Comments that can be made by other authenticated users")]
        public class Comment
        {
            [Key]
            public int CommentId { get; set; }
            public string Text { get; set; }

            [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            public DateTime CommentDate { get; set; }

            public Thing Thing { get; set; }
        }

        [Comment("TravelCompanions authorizes whether one person can view anothers' content")]
        public class TravelCompanion
        {
            [Key]
            public int Id { get; set; }
            public IdentityUser CompanionId1 { get; set; }
            public IdentityUser CompanionId2 { get; set; }
            public DateTime DateCreated { get; set; }

        }
    }
}