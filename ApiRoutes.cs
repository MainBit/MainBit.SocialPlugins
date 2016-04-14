using Orchard.Environment.Extensions;
using Orchard.Mvc.Routes;
using Orchard.WebApi.Routes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MainBit.SocialPlugins
{
    [OrchardFeature("MainBit.SocialPlugins.Twitter")]
    public class ApiRoutes : IHttpRouteProvider
    {
        public IEnumerable<RouteDescriptor> GetRoutes()
        {
            return new RouteDescriptor[] {
                new HttpRouteDescriptor {
                    Priority = 10,
                    RouteTemplate = "api/mainbit/twitter",
                    Defaults = new {
                        area = "MainBit.SocialPlugins",
                        controller = "TwitterApi"
                    }
                }
            };
        }

        public void GetRoutes(ICollection<RouteDescriptor> routes)
        {
            foreach (var routeDescriptor in GetRoutes())
                routes.Add(routeDescriptor);
        }
    }
}