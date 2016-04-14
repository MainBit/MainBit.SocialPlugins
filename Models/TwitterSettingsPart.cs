using Newtonsoft.Json;
using Orchard.ContentManagement;
using Orchard.Environment.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MainBit.SocialPlugins.Models
{
    [OrchardFeature("MainBit.SocialPlugins.Twitter")]
    public class TwitterSettingsPart : ContentPart
    {
        public string ConsumerKey
        {
            get { return this.Retrieve(x => x.ConsumerKey); }
            set { this.Store(x => x.ConsumerKey, value); }
        }
        public string ConsumerSecret
        {
            get { return this.Retrieve(x => x.ConsumerSecret); }
            set { this.Store(x => x.ConsumerSecret, value); }
        }
        public string AccessToken
        {
            get { return this.Retrieve(x => x.AccessToken); }
            set { this.Store(x => x.AccessToken, value); }
        }
        public string AccessTokenSecret
        {
            get { return this.Retrieve(x => x.AccessTokenSecret); }
            set { this.Store(x => x.AccessTokenSecret, value); }
        }

        public string TwitterUser
        {
            get { return this.Retrieve(x => x.TwitterUser); }
            set { this.Store(x => x.TwitterUser, value); }
        }

        public TwitterUser GetTwitterUser()
        {
            return JsonConvert.DeserializeObject<TwitterUser>(TwitterUser);
        }
    }

    public class TwitterUser
    {
        public long Id { get; set; }
        public string ScreenName { get; set; }
        public string AccessToken { get; set; }
        public string AccessTokenSecret { get; set; }
    }
}