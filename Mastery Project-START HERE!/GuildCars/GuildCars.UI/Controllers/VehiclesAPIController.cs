using GuildCars.Data.Factories;
using GuildCars.Models.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GuildCars.UI.Controllers
{
    public class VehiclesAPIController : ApiController
    {
        [AllowAnonymous]
        [Route("api/Inventory/New")]
        [AcceptVerbs("GET")]
        public IHttpActionResult SearchNewVehicles(decimal? minPrice, decimal? maxPrice, int? minYear, int? maxYear, string generalSearchParam)
        {
            var repo = VehicleRepoFactory.GetRepo();

            try
            {
                var parameters = new VehicleSearchParameters()
                {
                    MinPrice = minPrice,
                    MaxPrice = maxPrice,
                    MinYear = minYear,
                    MaxYear = maxYear,
                    GeneralSearchParam = generalSearchParam
                };
                var result = repo.SearchNew(parameters);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [AllowAnonymous]
        [Route("api/Inventory/Used")]
        [AcceptVerbs("GET")]
        public IHttpActionResult SearchUsedVehicles(decimal? minPrice, decimal? maxPrice, int? minYear, int? maxYear, string generalSearchParam)
        {
            var repo = VehicleRepoFactory.GetRepo();

            try
            {
                var parameters = new VehicleSearchParameters()
                {
                    MinPrice = minPrice,
                    MaxPrice = maxPrice,
                    MinYear = minYear,
                    MaxYear = maxYear,
                    GeneralSearchParam = generalSearchParam
                };
                var result = repo.SearchUsed(parameters);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [Authorize(Roles = "Sales")]
        [Route("api/Sales/Index")]
        [AcceptVerbs("GET")]
        public IHttpActionResult SalesSearchAllVehicles(decimal? minPrice, decimal? maxPrice, int? minYear, int? maxYear, string generalSearchParam)
        {
            var repo = VehicleRepoFactory.GetRepo();

            try
            {
                var parameters = new VehicleSearchParameters()
                {
                    MinPrice = minPrice,
                    MaxPrice = maxPrice,
                    MinYear = minYear,
                    MaxYear = maxYear,
                    GeneralSearchParam = generalSearchParam
                };
                var result = repo.SearchAll(parameters);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [Authorize(Roles = "Admin")]
        [Route("api/Admin/Vehicles")]
        [AcceptVerbs("GET")]
        public IHttpActionResult AdminSearchAllVehicles(decimal? minPrice, decimal? maxPrice, int? minYear, int? maxYear, string generalSearchParam)
        {
            var repo = VehicleRepoFactory.GetRepo();

            try
            {
                var parameters = new VehicleSearchParameters()
                {
                    MinPrice = minPrice,
                    MaxPrice = maxPrice,
                    MinYear = minYear,
                    MaxYear = maxYear,
                    GeneralSearchParam = generalSearchParam
                };
                var result = repo.SearchAll(parameters);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


    }
}
