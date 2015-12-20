using Orchard.Layouts.Framework.Display;
using Orchard.Layouts.Framework.Drivers;
using MainBit.SocialPlugins.Elements;
using MainBit.Utility.Services;
using System.Web.Mvc;
using Orchard.Mvc.Extensions;
using Orchard.Mvc.Html;

namespace MainBit.SocialPlugins.Drivers {
    public class ShareButtonsElementDriver : ElementDriver<ShareButtons>
    {
        private readonly ICurrentContentAccessor _currentContentAccessor;
        private readonly UrlHelper _urlHelper;

        public ShareButtonsElementDriver(
            ICurrentContentAccessor currentContentAccessor,
            UrlHelper urlHelper)
        {
            _currentContentAccessor = currentContentAccessor;
            _urlHelper = urlHelper;
        }

        
        protected override void OnDisplaying(ShareButtons element, ElementDisplayingContext context) {

            // requested (routed) content item url
            if(_currentContentAccessor.CurrentContentItem != null)
            {
                var requestedItemDisplayUrl = _urlHelper.ItemDisplayUrl(_currentContentAccessor.CurrentContentItem);
                context.ElementShape.RequestedItemDisplayUrl = requestedItemDisplayUrl;
                context.ElementShape.RequestedItemDisplayUrlAbsolute = _urlHelper.MakeAbsolute(requestedItemDisplayUrl);
            }

            // content item url witch contains this element (widget, content item from projection, ...)
            if (_currentContentAccessor.CurrentContentItem != null && _currentContentAccessor.CurrentContentItem.Id == context.Content.Id)
            {
                context.ElementShape.ItemDisplayUrl = context.ElementShape.RequestedItemDisplayUrl;
                context.ElementShape.ItemDisplayUrlAbsolute = context.ElementShape.RequestedItemAbsoluteDisplayUrl;
            }
            else
            {
                var itemDisplayUrl = _urlHelper.ItemDisplayUrl(context.Content);
                context.ElementShape.ItemDisplayUrl = itemDisplayUrl;
                context.ElementShape.ItemDisplayUrlAbsolute = _urlHelper.MakeAbsolute(itemDisplayUrl);
            }

            // current url
            // _urlHelper.MakeAbsolute - uses domain from site settings (not from request)
            var request = _urlHelper.RequestContext.HttpContext.Request;
            context.ElementShape.UrlAbsolute = _urlHelper.MakeAbsolute(request.Url.AbsolutePath);
            context.ElementShape.UrlAbsoluteWithoutQuery = _urlHelper.MakeAbsolute(request.Url.PathAndQuery);
        }
    }
}