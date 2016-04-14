using MainBit.SocialPlugins.Models;
using Orchard;
using Orchard.Caching;
using Orchard.ContentManagement;
using Orchard.Environment.Extensions;
using Orchard.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Tweetinvi;
using Tweetinvi.Core.Credentials;
using Tweetinvi.Core.Interfaces;
using Tweetinvi.Core.Parameters;

namespace MainBit.SocialPlugins.Controllers
{
    [OrchardFeature("MainBit.SocialPlugins.Twitter")]
    public class TwitterApiController : ApiController
    {
        private readonly IWorkContextAccessor _wca;
        private readonly ICacheManager _cacheManager;
        private readonly IClock _clock;

        public TwitterApiController(
            IWorkContextAccessor wca,
            ICacheManager cacheManager,
            IClock clock)
        {
            _wca = wca;
            _cacheManager = cacheManager;
            _clock = clock;
        }

        public IEnumerable<dynamic> Get(string url)
        {
            return _cacheManager.Get("Twitter_UserTimeline_4", true, ctx => {
                
                //ctx.Monitor(_signals.("MediaProcessing_Saved_" + profile));
                ctx.Monitor(_clock.When(TimeSpan.FromMinutes(1)));

                var settings = _wca.GetContext().CurrentSite.As<TwitterSettingsPart>();
                var twitterUser = settings.GetTwitterUser();

                var creds = new TwitterCredentials(settings.ConsumerKey, settings.ConsumerSecret, twitterUser.AccessToken, twitterUser.AccessTokenSecret);
                var tweets = Auth.ExecuteOperationWithCredentials(creds, () =>
                {
                    var userTimelineParameters = new UserTimelineParameters
                    {
                        ExcludeReplies = true,
                        MaximumNumberOfTweetsToRetrieve = 4,
                        TrimUser = true,
                    };
                    return Timeline.GetUserTimeline(twitterUser.Id, userTimelineParameters);
                });

                return tweets.Select(t => new { Id = t.IdStr });
            });

            
        }
    }
}