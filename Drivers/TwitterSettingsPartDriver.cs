using Orchard.ContentManagement;
using Orchard.ContentManagement.Drivers;
using Orchard.Localization;
using Orchard.Environment.Extensions;
using MainBit.SocialPlugins.Models;
using Orchard.Mvc;
using System.Web.Mvc;
using MainBit.SocialPlugins.ViewModels;
using Newtonsoft.Json;
using Tweetinvi.Core.Credentials;
using Tweetinvi;

namespace MainBit.SocialPlugins.Drivers
{
    [OrchardFeature("MainBit.SocialPlugins.Twitter")]
    public class SmtpSettingsPartDriver : ContentPartDriver<TwitterSettingsPart> {

        private readonly IHttpContextAccessor _hca;
        private readonly UrlHelper _urlHelper;
        public SmtpSettingsPartDriver(IHttpContextAccessor hca, UrlHelper urlHelper) {
            _hca = hca;
            _urlHelper = urlHelper;
            T = NullLocalizer.Instance;
        }

        public Localizer T { get; set; }

        protected override string Prefix { get { return "TwitterSettings"; } }

        protected override DriverResult Editor(TwitterSettingsPart part, dynamic shapeHelper) {
            return ContentShape("Parts_SocialPlugins_TwitterSettings_Edit", () =>
            {
                var viewModel = new TwitterSettingsEditVM
                {
                    ConsumerKey = part.ConsumerKey,
                    ConsumerSecret = part.ConsumerSecret,
                    AccessToken = part.AccessToken,
                    AccessTokenSecret = part.AccessTokenSecret
                };

                if(!string.IsNullOrEmpty(part.TwitterUser))
                {
                    viewModel.TwitterUser = JsonConvert.SerializeObject(JsonConvert.DeserializeObject(part.TwitterUser), Formatting.Indented);
                }

                if (!string.IsNullOrEmpty(part.ConsumerKey) && !string.IsNullOrEmpty(part.ConsumerSecret))
                {
                    var httpContext = _hca.Current();

                    // Step 1 : Redirect user to go on Twitter.com to authenticate
                    var appCreds = new ConsumerCredentials(part.ConsumerKey, part.ConsumerSecret);

                    // Specify the url you want the user to be redirected to
                    var redirectURL = _urlHelper.Action("AuthorizeCallback", "Twitter", new { area = "MainBit.SocialPlugins" }, httpContext.Request.Url.Scheme);

                    var twitterAuthUrl = CredentialsCreator.GetAuthorizationURL(appCreds, redirectURL);
                    viewModel.GetUserAccessTokenLink = twitterAuthUrl;
                }
                return shapeHelper.EditorTemplate(TemplateName: "Parts/SocialPlugins.TwitterSettings", Model: viewModel, Prefix: Prefix);
            })
            .OnGroup("twitter");
        }

        protected override DriverResult Editor(TwitterSettingsPart part, IUpdateModel updater, dynamic shapeHelper)
        {
            var viewModel = new TwitterSettingsEditVM();
            if (updater.TryUpdateModel(viewModel, Prefix, null, null))
            {
                part.ConsumerKey = viewModel.ConsumerKey;
                part.ConsumerSecret = viewModel.ConsumerSecret;
                part.AccessToken = viewModel.AccessToken;
                part.AccessTokenSecret = viewModel.AccessTokenSecret;
            }
            return Editor(part, shapeHelper);
        }
    }
}