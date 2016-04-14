using Orchard.Environment.Extensions;
using Orchard.Mvc.Routes;
using Orchard.WebApi.Routes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MainBit.SocialPlugins
{
    [OrchardFeature("MainBit.SocialPlugins.Twitter")]
    public class Routes : IRouteProvider
    {
        public void GetRoutes(ICollection<RouteDescriptor> routes)
        {
            foreach (var route in GetRoutes())
            {
                routes.Add(route);
            }
        }

        public IEnumerable<RouteDescriptor> GetRoutes()
        {
            yield return new RouteDescriptor
            {
                Priority = 10,
                Route = new Route(
                    "mainbit/twitter/authorize-callback",
                    new RouteValueDictionary {
                        {"area", "MainBit.SocialPlugins"},
                        {"controller", "Twitter"},
                        {"action", "AuthorizeCallback"}
                    },
                    null,
                    new RouteValueDictionary() {
                        {"area", "MainBit.SocialPlugins"}
                    },
                    new MvcRouteHandler())
            };
        }
    }
}