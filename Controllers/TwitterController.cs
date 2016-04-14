using MainBit.SocialPlugins.Models;
using Newtonsoft.Json;
using Orchard;
using Orchard.ContentManagement;
using Orchard.Environment.Extensions;
using System.Web.Mvc;
using Tweetinvi;
using Tweetinvi.Core.Credentials;

namespace MainBit.SocialPlugins.Controllers
{
    [OrchardFeature("MainBit.SocialPlugins.Twitter")]
    public class TwitterController : Controller
    {
        private readonly IWorkContextAccessor _wca;
        public TwitterController(IWorkContextAccessor wca)
        {
            _wca = wca;
        }

        public ActionResult AuthorizeCallback()
        {
            var settings = _wca.GetContext().CurrentSite.As<TwitterSettingsPart>();

            // Get some information back from the URL
            var verifierCode = Request.Params.Get("oauth_verifier");
            var authorizationId = Request.Params.Get("authorization_id");

            var appCredentials = new ConsumerCredentials(settings.ConsumerKey, settings.ConsumerSecret);
            var userCredentials = CredentialsCreator.GetCredentialsFromVerifierCode(verifierCode, authorizationId);
            var user = Tweetinvi.User.GetLoggedUser(userCredentials); // Tweetinvi.User.GetAuthenticatedUser(userCredentials);

            var twitterUser = new TwitterUser
            {
                Id = user.Id,
                ScreenName = user.ScreenName,
                AccessToken = userCredentials.AccessToken,
                AccessTokenSecret = userCredentials.AccessTokenSecret
            };

            settings.TwitterUser = JsonConvert.SerializeObject(twitterUser);

            return new RedirectResult("~/Admin/Settings/Twitter");
        }
    }
}