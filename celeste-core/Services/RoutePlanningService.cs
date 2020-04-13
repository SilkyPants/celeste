
using System;
using System.Collections.Generic;
using System.Linq;
using Celeste.Models;

namespace Celeste.Services
{
    public class RoutePlanningService
    {
        List<Route> _routes = new List<Route>();

        public RoutePlanningService() 
        {
            var route = new Route() {
                Id = 345,
                Systems = {
                    new RouteSystem() {
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

        static int index = 0;
        internal IEnumerable<Models.Route> GetRoutes()
        {
            return _routes.AsReadOnly();
        }

        internal Models.Route GetRouteWithId(int id)
        {
            return _routes.First( (x) => x.Id == id );
        }

        internal void DeleteRouteWithId(int id)
        {
            var routeIdx = _routes.FindIndex(x => x.Id == id);
            _routes.RemoveAt(routeIdx);
        }

        internal int AddRoute(Route route)
        { 
            route.Id = index++;
            _routes.Add(route);

            return route.Id;
        }
    }
}