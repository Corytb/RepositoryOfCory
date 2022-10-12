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
    public class ReportsAPIController : ApiController
    {
        [Route("api/Reports/SalesReports_Reports")]
        [AcceptVerbs("GET")]
        public IHttpActionResult SearchSalesReports(DateTime? minDate, DateTime? maxDate, string userId)
        {
            var repo = UserRepoFactory.GetRepo();

            try
            {
                var parameters = new SalesReportSearchParameters()
                {
                    MinDate = minDate,
                    MaxDate = maxDate,
                    UserId = userId
                };
                var result = repo.SearchSalesReports(parameters);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}