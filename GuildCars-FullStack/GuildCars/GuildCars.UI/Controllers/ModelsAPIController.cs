using GuildCars.Data.Factories;
using GuildCars.Models.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace GuildCars.UI.Controllers
{
    public class ModelsAPIController : ApiController
    {
        [AllowAnonymous]
        [Route("api/Search/Models")]
        [AcceptVerbs("GET")]
        public IHttpActionResult GetVehicleModelsByMakeId(int carMakeId)
        {
            var repo = CarModelRepoFactory.GetRepo();

            try
            {
                var parameters = new CarMake()
                {
                    CarMakeId = carMakeId
                };
                var result = repo.GetModelsByMake(parameters.CarMakeId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}