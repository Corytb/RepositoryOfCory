using IAmTraveling.Models.CommandAndQueryModels.CommandModels;
using IAmTraveling.Models.CommandAndQueryModels.QueryModels;
using static IAmTraveling.Data.ApplicationDbContext;

namespace IAmTraveling.Data.Interfaces
{
    public interface IThingsRepo
    {
        IEnumerable<Thing> GetAllMyThings(string userId);
        void InsertNewThing(ThingInsertCommand thing);
        ThingDetailsQuery GetThingById(string userId, int thingId);
        IEnumerable<ThingLocationsQuery> GetThingLocations(string userId);
        void UpdateThing(ThingUpdateCommand thing);
        void DeleteThing(string userId, int thingId);
    }
}
