using Orchard.Layouts.Framework.Elements;

namespace MainBit.SocialPlugins.Elements
{
    public class ShareButtons : Element
    {
        public override string Category
        {
            get { return "Social"; }
        }

        public override bool HasEditor
        {
            get { return false; }
        }
    }
}