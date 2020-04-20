using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Celeste.Services;
using Celeste.Models;
using Celeste.Services.Spanch;
using System.Threading.Tasks;
using System.IO;
using System.Threading;
using Celeste.Models.Spansh;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Celeste.Controllers
{
    [Route("api/[controller]")]
    public class RoutePlanningController : Controller
    {
        private readonly RoutePlanningService _planningService;
        private readonly IRoadToRichesRouteService _spanshService;

        public RoutePlanningController(RoutePlanningService planningService, IRoadToRichesRouteService spanshService)
        {
            this._planningService = planningService;
            this._spanshService = spanshService;
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

        // POST api/<controller>/spansh/csv
        [HttpPost("spansh/csv")]
        public async Task<IActionResult> ReadStringDataManual()
        {
            using (StreamReader reader = new StreamReader(Request.Body, System.Text.Encoding.UTF8))
            {  
                var csvData = await reader.ReadToEndAsync();
                try
                {
                    if (string.IsNullOrWhiteSpace(csvData) || !ModelState.IsValid)
                    {
                        return BadRequest("Cannot add empty route");
                    }

                    var route = this._planningService.R2RCsvToRoute(csvData);
                    var id = this._planningService.AddRoute(route);
                    return Ok(id);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
        }

        // POST api/<controller>/spansh
        [HttpPost("spansh/riches")]
        public async Task<IActionResult> GenerateR2RRoute([FromBody]Models.Spansh.R2RRouteParameters parameters)
        {
            var jobId = await this._spanshService.GenerateRoute(parameters);

            R2RRouteResponse routeResponse;
            do {
                Thread.Sleep(5000);
                routeResponse = await this._spanshService.PollJob(jobId);
                routeResponse.JobId ??= jobId; // Successful response does not return the Guid
            } while (!routeResponse.JobComplete);

            var id = this._planningService.AddRoute(routeResponse.ToRoute());
            return Ok(id);
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