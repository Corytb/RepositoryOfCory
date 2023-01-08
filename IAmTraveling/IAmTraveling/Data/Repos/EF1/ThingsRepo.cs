using IAmTraveling.Data.Interfaces;
using IAmTraveling.Models.CommandAndQueryModels.CommandModels;
using IAmTraveling.Models.CommandAndQueryModels.QueryModels;
using static IAmTraveling.Data.ApplicationDbContext;

namespace IAmTraveling.Data.Repos.EF1
{
    public class ThingsRepo : IThingsRepo
    {
        private ApplicationDbContext context;
        public ThingsRepo(ApplicationDbContext context)
        {
            this.context = context;
        }


        public void DeleteThing(string userId, int thingId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Thing> GetAllMyThings(string userId)
        {

            //var things = context.Things.Where(a => a.UserId.Equals(userId)).ToList();

            var things = from thing in context.Things
                          join user in context.Users on thing.UserId equals user.Id
                          //where user.Id equals userId
                          select new ThingQuery
                          {
                              Id = thing.UserId,
                              ThingId = thing.ThingId,
                              ThingDate = thing.ThingDate
                          };

            return things;

        }

        public ThingDetailsQuery GetThingById(string userId, int thingId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ThingLocationsQuery> GetThingLocations(string userId)
        {
            throw new NotImplementedException();
        }

        public void InsertNewThing(ThingInsertCommand thing)
        {
            throw new NotImplementedException();
        }

        public void UpdateThing(ThingUpdateCommand thing)
        {
            throw new NotImplementedException();
        }
    }
}
