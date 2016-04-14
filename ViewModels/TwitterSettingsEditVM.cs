using Orchard.ContentManagement;
using Orchard.Environment.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MainBit.SocialPlugins.ViewModels
{
    public class TwitterSettingsEditVM
    {
        public string ConsumerKey { get; set; }
        public string ConsumerSecret { get; set; }
        public string AccessToken { get; set; }
        public string AccessTokenSecret { get; set; }


        public string TwitterUser { get; set; }

        public string GetUserAccessTokenLink { get; set; }
    }
}