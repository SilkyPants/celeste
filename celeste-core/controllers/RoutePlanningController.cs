using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Celeste.Services;
using Celeste.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Celeste.Controllers
{
    [Route("api/[controller]")]
    public class RoutePlanningController : Controller
    {
        private readonly RoutePlanningService _planningService;

        public RoutePlanningController(RoutePlanningService planningService)
        {
            this._planningService = planningService;
        }

        // GET: api/<controller>
        [HttpGet]
        public IEnumerable<Models.Route> Get()
        {
            return this._planningService.GetRoutes();
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public Models.Route Get(Guid id)
        {
            return this._planningService.GetRouteWithId(id: id);
        }

        // POST api/<controller>
        [HttpPost]
        public IActionResult Post([FromBody]Route route)
        {
            try
            {
                if (route == null || !ModelState.IsValid)
                {
                    return BadRequest("Cannot add empty route");
                }

                var id = this._planningService.AddRoute(route);
                return Ok(id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST api/<controller>
        [HttpPost]
        [Route("api/[controller]/csv")]
        public IActionResult Post([FromBody]string value)
        {
            // TODO: Parse CSV input to Route, Add, then return ID
            return Ok();
        }

        // POST api/<controller>
        [HttpPost]
        [Route("api/[controller]/spansh")]
        public IActionResult Post([FromBody]Models.Spansh.R2RRouteParameters parameters)
        {
            // TODO: Parse CSV input to Route, Add, then return ID
            return Ok();
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public IActionResult Put(Guid id, [FromBody]Route updatedRoute)
        {
            if (id != updatedRoute.Id) return BadRequest("Id mismatch with route");
            
            this._planningService.UpdateRoute(id, updatedRoute);

            return Ok(updatedRoute);
        }

        // PUT api/<controller>/5
        [HttpPut("{routeId}/visited/{bodyId}")]
        public IActionResult MarkBodyVisited(Guid routeId, string bodyId)
        {
            if (!this._planningService.MarkBodyVisited(routeId, bodyId)) return BadRequest();
            return Ok();
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            this._planningService.DeleteRouteWithId(id: id);
            return Ok();
        }
    }
}