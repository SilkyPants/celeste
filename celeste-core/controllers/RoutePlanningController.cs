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
        public Models.Route Get(int id)
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
                    return BadRequest("ErrorCode.TodoItemNameAndNotesRequired.ToString()");
                }
                // TODO: Parse CSV input to Route, Add, then return ID
                var id = this._planningService.AddRoute(route);

                if (id < 0)
                    return Ok();

                return Ok(id);
            }
            catch (Exception)
            {
                return BadRequest("ErrorCode.CouldNotUpdateItem.ToString()");
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

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]string value)
        {
            // TODO: Not sure what to do here
            // Do we have multiple PUTs where we update depending on type? 
            return Ok();
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            this._planningService.DeleteRouteWithId(id: id);
            return Ok();
        }
    }
}