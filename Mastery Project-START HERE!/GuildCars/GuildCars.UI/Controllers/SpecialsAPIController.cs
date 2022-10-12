using GuildCars.Data.Factories;
using GuildCars.Models.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace GuildCars.UI.Controllers
{
    [Authorize(Roles = "Admin")]
    public class SpecialsAPIController : ApiController
    {
        [Route("api/Admin/Specials_Admin/{specialId}")]
        [AcceptVerbs("DELETE")]
        public IHttpActionResult DeleteSpecial(int specialId)
        {
            var repo = SpecialRepoFactory.GetRepo();

            try
            {
                repo.SpecialsDelete(specialId);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}