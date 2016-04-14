using MainBit.SocialPlugins.Models;
using Orchard.ContentManagement;
using Orchard.ContentManagement.Handlers;
using Orchard.Environment.Extensions;
using Orchard.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MainBit.SocialPlugins.Handlers
{
    [OrchardFeature("MainBit.SocialPlugins.Twitter")]
    public class TwitterSettingsPartHandler : ContentHandler
    {
        public TwitterSettingsPartHandler()
        {
            T = NullLocalizer.Instance;
            Filters.Add(new ActivatingFilter<TwitterSettingsPart>("Site"));
            //Filters.Add(new TemplateFilterForPart<TwitterSettingsPart>("TwitterSettings", "Parts/SocialPlugins.TwitterSettings", "twitter"));
        }

        public Localizer T { get; set; }

        protected override void GetItemMetadata(GetContentItemMetadataContext context)
        {
            if (context.ContentItem.ContentType != "Site")
                return;
            base.GetItemMetadata(context);
            context.Metadata.EditorGroupInfo.Add(new GroupInfo(T("Twitter")));
        }
    }
}