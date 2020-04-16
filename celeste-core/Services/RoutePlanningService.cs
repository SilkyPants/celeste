
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Celeste.Models;

namespace Celeste.Services
{
    public class RoutePlanningService
    {
        List<Route> _routes = new List<Route>();

        public RoutePlanningService() 
        {
            var route = new Route() {
                Id = Guid.NewGuid(),
                Systems = {
                    new StarSystem() {
                        Name = "Bobs",
                        Bodies = {
                            new Body() {
                                Name = "Susan",
                                EstimatedMappingValue = 123123
                            }
                        },
                        Jumps = 1220
                    }
                }
            };

            _routes = new List<Route>();
            _routes.Add(route);
        }

        internal IEnumerable<Models.Route> GetRoutes()
        {
            return _routes.AsReadOnly();
        }

        internal Models.Route GetRouteWithId(Guid id)
        {
            return _routes.FirstOrDefault(x => x.Id == id);
        }

        internal void DeleteRouteWithId(Guid id)
        {
            var routeIdx = _routes.FindIndex(x => x.Id == id);
            if (routeIdx > -1)
                _routes.RemoveAt(routeIdx);
        }

        internal string AddRoute(Route route)
        {
            if (GetRouteWithId(route.Id) != null) throw new System.InvalidOperationException("Id is already in use!");;
            _routes.Add(route);

            return route.Id.ToString();
        }

        internal bool UpdateRoute(Guid id, Route updatedRoute) {
            var routeIdx = _routes.FindIndex(x => x.Id == id);
            if (routeIdx <= -1)
                return false;

            _routes[routeIdx] = updatedRoute;
            return true;
        }

        internal bool MarkBodyVisited(Guid routeId, string bodyId64)
        {
            // TODO: This should mark as visited in the DB
            // Flatten list of bodies
            var bodies = _routes.Select<Route, List<Body>>((route, _) => {
                return route.Systems.SelectMany((system, _) => {
                    return system.Bodies;
                }).ToList();
            }).SelectMany(d => d).ToList();

            var body = bodies?.FirstOrDefault(b => b.Id64 == bodyId64);
            if (body == null) return false;
            
            body.Visited = true;
            return true;
        }
    }
}
    